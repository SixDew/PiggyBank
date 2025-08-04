namespace PiggyBank.DTO
{
    public record TransactionFromClientDto(
        Guid Id,
        Guid WalletId,
        decimal Amount,
        TransactionDirection? Direction,
        DateTimeOffset OccuredAt,
        string? Comment)
    { }

    public enum TransactionDirection : short
    {
        Income,
        Expense
    }
}
