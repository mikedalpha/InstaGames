using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class UserGameRatingsRepos : Repository<UserGameRatings>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public UserGameRatingsRepos(ApplicationDbContext context) : base(context)
        {

        }
    }
}
