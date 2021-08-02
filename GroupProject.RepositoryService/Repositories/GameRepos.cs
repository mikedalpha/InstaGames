using System.Linq;
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

        public bool GameExists(int id)
        {
            return DbContext.Games.Count(g => g.GameId == id) > 0;
        }

    }
}
