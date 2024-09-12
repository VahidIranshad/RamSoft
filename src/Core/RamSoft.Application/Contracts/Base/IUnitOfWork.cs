using RamSoft.Application.Contracts.Jira;

namespace RamSoft.Application.Contracts.Base
{
    public interface IUnitOfWork : IDisposable
    {

        IStatesRepository StatesRepository { get; }

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
