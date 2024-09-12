using RamSoft.Domain.Base;
using System.Linq.Expressions;

namespace RamSoft.Application.Contracts.Base
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<IReadOnlyList<T>> GetAll(CancellationToken cancellationToken);
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<List<T>> Get(List<int> ids, CancellationToken cancellationToken);
        Task<(IQueryable<T>, int)> Get(string Filter, string Order, int? PageNumber, int? PageSize, CancellationToken cancellationToken, bool? disableTracking = true);
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<bool> Exists(int id, CancellationToken cancellationToken);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Delete(T entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Delete(List<int> ids, CancellationToken cancellationToken);
        T AddEntity(T entity, CancellationToken cancellationToken);
        List<T> AddRangeEntity(List<T> list, CancellationToken cancellationToken);
        Task<List<T>> AddRangeEntityAsync(List<T> list, CancellationToken cancellationToken);
    }
}
