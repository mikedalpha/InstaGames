using System.Threading.Tasks;
using GroupProject.Database;
using GroupProject.RepositoryService.Repositories;

namespace GroupProject.RepositoryService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public GameRepos Games { get; }
        public DeveloperRepos Developer { get; }
        public CategoryRepos Category { get; }
        public MessageRepos Message { get; }
        public PegiRepos Pegi { get; }
        public UserGameRatingsRepos UserGameRatings { get; }

        public UnitOfWork()
        {
            context = new ApplicationDbContext();
            Games = new GameRepos(context);
            Developer = new DeveloperRepos(context);
            Category = new CategoryRepos(context);
            Message = new MessageRepos(context);
            Pegi = new PegiRepos(context);
            UserGameRatings = new UserGameRatingsRepos(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
