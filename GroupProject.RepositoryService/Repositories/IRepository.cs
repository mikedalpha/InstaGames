using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.RepositoryService.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity FindById(int? id);
        IEnumerable<TEntity> Get();
    }
}
