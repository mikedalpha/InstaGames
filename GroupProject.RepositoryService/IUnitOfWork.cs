using System;
using GroupProject.RepositoryService.Repositories;

namespace GroupProject.RepositoryService
{
    public interface IUnitOfWork : IDisposable
    {
        GameRepos Games { get; }
        DeveloperRepos Developer { get; }
        CategoryRepos Category { get; }

        void Save();
    }
}
