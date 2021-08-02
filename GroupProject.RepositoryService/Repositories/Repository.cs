using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GroupProject.RepositoryService.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity FindById(int? id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(int? id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }  
        
        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        public void Create(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        public void Edit(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
