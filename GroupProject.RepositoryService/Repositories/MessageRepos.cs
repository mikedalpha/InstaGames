using System.Data.Entity;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class MessageRepos : Repository<Message>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public MessageRepos(DbContext context) : base(context)
        {
        }
    }
}
