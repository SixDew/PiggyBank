using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyBank.Converters;
using PiggyBank.DTO;
using PiggyBank.Models;
using PiggyBank.Repositories;
using PiggyBank.Resources;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController(IValidator<Transaction> validator,
        ITransactionRepository transactionRepository, IWalletRepository walletRepository,
        ITransactionConverter converter) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TransactionsListDto>> GetTransactions([FromQuery] Guid? walletId,
            [FromQuery] DateTimeOffset? from, [FromQuery] DateTimeOffset? to)
        {
            var transactions = await transactionRepository.GetAllAsync(walletId, from, to);
            return Ok(new TransactionsListDto()
            {
                Data = transactions.Select(converter.Convert).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> AddTransaction([FromBody] Transaction data)
        {
            validator.ValidateAndThrow(data);
            var wallet = await walletRepository.GetAsync(data.WalletId);
            if (wallet is null) return WalletNotExistProblem();

            switch (data.Direction)
            {
                case TransactionDirection.Income:
                    {
                        wallet.Balance += data.Amount;
                        break;
                    }
                case TransactionDirection.Expense:
                    {
                        wallet.Balance -= data.Amount;
                        break;
                    }
            }

            var transactionModel = await transactionRepository.GetAsync(data.Id);
            if (transactionModel is not null) return TransactionAlreadyExistProblem();

            transactionModel = converter.Convert(data);
            await transactionRepository.AddAsync(transactionModel);

            ChangeBalance(wallet, transactionModel);

            try
            {
                await transactionRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return DbUpdateVersionProblem();
            }

            return CreatedAtAction(nameof(GetTransactions), new { walletId = transactionModel.Id },
                new TransactionDto() { Data = converter.Convert(transactionModel) });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(Guid id)
        {
            var transaction = await transactionRepository.GetAsync(id);
            if (transaction is null) return TransactionNotExistProblem();

            var wallet = await walletRepository.GetAsync(transaction.Id);
            if (wallet is null) return WalletNotExistProblem();

            ChangeBalance(wallet, transaction);

            transactionRepository.Delete(transaction);
            try
            {
                await transactionRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return DbUpdateVersionProblem();
            }

            return Ok();
        }

        private ObjectResult WalletNotExistProblem()
        {
            return NotFound(new TransactionDto()
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
            return NotFound(new TransactionDto()
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
            return Conflict(new TransactionDto()
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
            return Conflict(new TransactionDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Balance has been changed before, repeat operation",
                    Status = 409,
                    Title = "Conflict"
                }
            });
        }

        private void ChangeBalance(Wallets wallet, Transactions transaction)
        {
            wallet.Balance += transaction.Direction switch
            {
                TransactionDirection.Income => transaction.Amount,
                TransactionDirection.Expense => -transaction.Amount,
                _ => throw new ValidationException("Direction is invalid")
            };
        }
    }
}
