namespace PiggyBank.Services.Interfaces
{
    public interface IBalanceCounter
    {
        int Veiws { get; }
        int IncViews();
    }
}
