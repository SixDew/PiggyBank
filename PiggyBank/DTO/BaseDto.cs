namespace PiggyBank.DTO
{
    public record BaseDto<TData, TError>()
    {
        public TData? Data { get; init; }
        public TError? Error { get; init; }
    }
}
