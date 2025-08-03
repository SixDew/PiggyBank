namespace PiggyBank.DTO
{
    public record BaseDto()
    {
        public object? Data { get; init; }
        public object? Error { get; init; }
    }
}
