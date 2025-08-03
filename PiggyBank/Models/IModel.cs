namespace PiggyBank.Models
{
    public interface IModel<TId>
    {
        TId Id { get; set; }
    }
}
