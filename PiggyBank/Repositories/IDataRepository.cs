using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public interface IDataRepository<T, TId> where T : IModel<TId>
    {
        Task<T?> GetAsync(TId id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T data);
        void Delete(T data);
        Task DeleteAsync(TId id);
        void Update(T data);
        Task SaveChangesAsync();
    }
}
