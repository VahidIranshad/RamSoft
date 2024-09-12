using RamSoft.Domain.Base;

namespace RamSoft.Application.Contracts.Base
{
    public interface IUnitOfWork<T> : IDisposable
         where T : BaseEntity
    {

        IGenericRepository<T> Repository();

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
