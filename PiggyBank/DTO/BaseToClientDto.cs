namespace PiggyBank.DTO
{
    public record BaseToClientDto<TData, TError>()
    {
        public TData? Data { get; init; }
        public TError? Error { get; init; }
    }
}
