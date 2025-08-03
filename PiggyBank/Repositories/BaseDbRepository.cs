using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public class BaseDbRepository<TModel, TContext> : IDataRepository<TModel, Guid> where TModel : BaseModel where TContext : DbContext
    {
        protected DbContext Context { get; init; }
        protected DbSet<TModel> Set { get; init; }
        public BaseDbRepository(TContext context)
        {
            Context = context;
            Set = context.Set<TModel>();
        }

        public async Task AddAsync(TModel data)
        {
            await Set.AddAsync(data);
        }

        public void Delete(TModel data)
        {
            Set.Remove(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await GetAsync(id);
            if (data is not null) Set.Remove(data);
        }

        public async Task<List<TModel>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<TModel?> GetAsync(Guid id)
        {
            return await Set.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Update(TModel data)
        {
            Set.Update(data);
        }
    }
}
