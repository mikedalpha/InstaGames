using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class GameRepos:Repository<Game>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public GameRepos(ApplicationDbContext context) : base(context)
        {

        }
    }
}
