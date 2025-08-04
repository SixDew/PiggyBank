using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyBank.Converters.Interfaces;
using PiggyBank.DTO;
using PiggyBank.Models;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController(IValidator<TransactionFromClientDto> validator,
        ITransactionRepository transactionRepository, IWalletRepository walletRepository,
        ITransactionConverter converter) : ControllerBase
    {
        //Получение транзакций с фильтрацией
        [HttpGet]
        public async Task<ActionResult<TransactionsListToClientDto>> GetTransactions([FromQuery] Guid? walletId,
            [FromQuery] DateTimeOffset? from, [FromQuery] DateTimeOffset? to,
            CancellationToken cancellation)
        {
            var transactions = await transactionRepository.GetAllAsync(cancellation, walletId, from, to);
            return Ok(new TransactionsListToClientDto()
            {
                Data = transactions.Select(converter.Convert).ToList()
            });
        }

        //Добавление транзакции
        [HttpPost]
        public async Task<ActionResult<TransactionToClientDto>> AddTransaction([FromBody] TransactionFromClientDto data,
            CancellationToken cancellation)
        {
            validator.ValidateAndThrow(data);
            var wallet = await walletRepository.GetAsync(data.WalletId, cancellation);
            if (wallet is null) return WalletNotExistProblem();

            var transactionModel = await transactionRepository.GetAsync(data.Id, cancellation);
            if (transactionModel is not null) return TransactionAlreadyExistProblem();

            transactionModel = converter.Convert(data);
            await transactionRepository.AddAsync(transactionModel, cancellation);

            ChangeBalance(wallet, transactionModel);

            //Отслеживаем конкуренцию
            try
            {
                await transactionRepository.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                return DbUpdateVersionProblem();
            }

            return CreatedAtAction(nameof(GetTransactions), new { walletId = transactionModel.Id },
                new TransactionToClientDto() { Data = converter.Convert(transactionModel) });
        }

        //Отмена транзакции
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(Guid id, CancellationToken cancellation)
        {
            var transaction = await transactionRepository.GetAsync(id, cancellation);
            if (transaction is null) return TransactionNotExistProblem();

            var wallet = await walletRepository.GetAsync(transaction.WalletId, cancellation);
            if (wallet is null) return WalletNotExistProblem();

            ChangeBalance(wallet, transaction, true);

            //Отслеживаем конкуренцию
            transactionRepository.Delete(transaction);
            try
            {
                await transactionRepository.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                return DbUpdateVersionProblem();
            }

            return Ok();
        }

        private ObjectResult WalletNotExistProblem()
        {
            return NotFound(new TransactionToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Wallet does not exist",
                    Status = 404,
                    Title = "NotFound"
                }
            });
        }

        private ObjectResult TransactionNotExistProblem()
        {
            return NotFound(new TransactionToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Transaction is not exist",
                    Status = 404,
                    Title = "NotFound"
                }
            });
        }

        private ObjectResult TransactionAlreadyExistProblem()
        {
            return Conflict(new TransactionToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Transaction with this Id already exists",
                    Status = 409,
                    Title = "Conflict"
                }
            });
        }

        private ObjectResult DbUpdateVersionProblem()
        {
            return Conflict(new TransactionToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Balance has been changed before, repeat operation",
                    Status = 409,
                    Title = "Conflict"
                }
            });
        }

        private void ChangeBalance(Wallets wallet, Transactions transaction, bool isRollback = false)
        {
            decimal amount = transaction.Direction switch
            {
                TransactionDirection.Income => transaction.Amount,
                TransactionDirection.Expense => -transaction.Amount,
                _ => throw new ValidationException("Direction is invalid")
            };

            if (isRollback) amount = -amount;

            wallet.Balance += amount;
        }
    }
}
