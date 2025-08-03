namespace PiggyBank.Resources
{
    public record Transaction(
        Guid Id,
        Guid WalletId,
        decimal Amount,
        TransactionDirection Direction,
        DateTimeOffset OccuredAt,
        string? Comment)
    { }

    public enum TransactionDirection : short
    {
        Income,
        Expense
    }
}
