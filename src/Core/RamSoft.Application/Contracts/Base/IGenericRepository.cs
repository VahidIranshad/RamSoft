using RamSoft.Domain.Base;
using System.Linq.Expressions;

namespace RamSoft.Application.Contracts.Base
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> Get(List<int> ids);
        Task<(IQueryable<T>, int)> Get(string Filter, string Order, int? PageNumber, int? PageSize, bool? disableTracking = true);
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(int id);
        Task Delete(List<int> ids);
        T AddEntity(T entity);
        List<T> AddRangeEntity(List<T> list);
        Task<List<T>> AddRangeEntityAsync(List<T> list);
    }
}
