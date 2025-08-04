namespace PiggyBank.Converters.Interfaces
{
    public interface IConverter<TIn, TOut>
    {
        TOut Convert(TIn data);
    }
}
