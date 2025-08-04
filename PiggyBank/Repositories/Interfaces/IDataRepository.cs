using PiggyBank.Models.Interfaces;

namespace PiggyBank.Repositories.Interfaces
{
    public interface IDataRepository<T, TId> where T : IModel<TId>
    {
        Task<T?> GetAsync(TId id, CancellationToken cancellation);
        Task<List<T>> GetAllAsync(CancellationToken cancellation);
        Task AddAsync(T data, CancellationToken cancellation);
        void Delete(T data);
        Task DeleteAsync(TId id, CancellationToken cancellation);
        void Update(T data);
        Task SaveChangesAsync(CancellationToken cancellation);
    }
}
