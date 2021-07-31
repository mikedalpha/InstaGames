using System.Data.Entity;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class PegiRepos :Repository<Pegi>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public PegiRepos(DbContext context) : base(context)
        {
        }
    }
}
