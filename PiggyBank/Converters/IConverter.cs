namespace PiggyBank.Converters
{
    public interface IConverter<TIn, TOut>
    {
        TOut Convert(TIn data);
    }
}
