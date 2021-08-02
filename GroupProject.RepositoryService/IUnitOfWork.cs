using System;
using System.Threading.Tasks;
using GroupProject.RepositoryService.Repositories;

namespace GroupProject.RepositoryService
{
    public interface IUnitOfWork : IDisposable
    {
        GameRepos Games { get; }
        DeveloperRepos Developer { get; }
        CategoryRepos Category { get; }
        MessageRepos Message { get; }
        PegiRepos Pegi { get; }

        void Save();

        Task<int> SaveAsync();
    }
}
