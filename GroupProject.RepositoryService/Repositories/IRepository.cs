using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupProject.RepositoryService.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity FindById(int? id);
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(int? id);
        void Attach(TEntity entity);
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Remove(TEntity entity);
    }
}
