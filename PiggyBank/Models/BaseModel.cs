using PiggyBank.Models.Interfaces;

namespace PiggyBank.Models
{
    public abstract class BaseModel : IModel<Guid>
    {
        public virtual Guid Id { get; set; }
    }
}
