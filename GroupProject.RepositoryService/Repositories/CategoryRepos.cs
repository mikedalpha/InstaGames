using GroupProject.Database;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class CategoryRepos: Repository<Category>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public CategoryRepos(ApplicationDbContext context) : base(context)
        {

        }
    }
}
