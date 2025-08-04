using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Repositories.Abstract
{
    public abstract class BaseDbRepository<TModel, TContext> : IDataRepository<TModel, Guid> where TModel : BaseModel where TContext : DbContext
    {
        protected DbContext Context { get; init; }
        protected DbSet<TModel> Set { get; init; }
        public BaseDbRepository(TContext context)
        {
            Context = context;
            Set = context.Set<TModel>();
        }

        public async Task AddAsync(TModel data, CancellationToken cancellation)
        {
            await Set.AddAsync(data, cancellation);
        }

        public void Delete(TModel data)
        {
            Set.Remove(data);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var data = await GetAsync(id, cancellation);
            if (data is not null) Set.Remove(data);
        }

        public async Task<List<TModel>> GetAllAsync(CancellationToken cancellation)
        {
            return await Set.ToListAsync(cancellation);
        }

        public async Task<TModel?> GetAsync(Guid id, CancellationToken cancellation)
        {
            return await Set.FirstOrDefaultAsync(d => d.Id == id, cancellation);
        }

        public async Task SaveChangesAsync(CancellationToken cancellation)
        {
            await Context.SaveChangesAsync(cancellation);
        }

        public void Update(TModel data)
        {
            Set.Update(data);
        }
    }
}
