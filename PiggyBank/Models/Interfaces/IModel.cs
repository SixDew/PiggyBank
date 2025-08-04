namespace PiggyBank.Models.Interfaces
{
    public interface IModel<TId>
    {
        TId Id { get; set; }
    }
}
