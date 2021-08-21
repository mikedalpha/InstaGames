using GroupProject.Database;
using System.Linq;
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
        public bool CategoryExists(int id)
        {
            return DbContext.Categories.Count(g => g.CategoryId == id) > 0;
        }
    }
}
