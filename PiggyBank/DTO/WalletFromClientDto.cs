namespace PiggyBank.DTO
{
    public record WalletFromClientDto(Guid Id, string Name, string Currency, decimal Balance) { }
}
