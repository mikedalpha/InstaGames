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

        public UnitOfWork()
        {
            context = new ApplicationDbContext();
            Games = new GameRepos(context);
            Developer = new DeveloperRepos(context);
            Category = new CategoryRepos(context);
            Message = new MessageRepos(context);
            Pegi = new PegiRepos(context);
        }

        public void Save()
        {
            context.SaveChanges();
        } 
        public void SaveAsync()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
