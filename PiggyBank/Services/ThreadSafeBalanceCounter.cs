using PiggyBank.Services.Interfaces;

namespace PiggyBank.Services
{
    public class ThreadSafeBalanceCounter : IBalanceCounter
    {
        int _views = 0;
        public int Veiws { get => _views; }

        public int IncViews()
        {
            Interlocked.Increment(ref _views);
            return _views;
        }
    }
}
