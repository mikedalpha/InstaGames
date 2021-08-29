using System.Linq;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class DeveloperRepos:Repository<Developer>
    {

        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public DeveloperRepos(ApplicationDbContext context) : base(context)
        {

        }

        public bool DeveloperExists(int id)
        {
            return DbContext.Developers.Count(d => d.DeveloperId == id) > 0;
        }


    }
}
