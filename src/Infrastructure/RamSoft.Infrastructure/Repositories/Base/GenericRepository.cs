using Fop;
using Fop.FopExpression;
using Microsoft.EntityFrameworkCore;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Domain.Base;
using RamSoft.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace RamSoft.Infrastructure.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly JiraDbContext _dbContext;
        public GenericRepository(JiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IReadOnlyList<T>> GetAll(CancellationToken cancellationToken, bool disableTracking = true)
        {
            if (disableTracking)
            {
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
            }
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }
        public async Task<T> Get(int id, CancellationToken cancellationToken, bool disableTracking = true)
        {
            if (disableTracking)
            {
                var result = await _dbContext.Set<T>().AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return result;
            }
            var data = await _dbContext.Set<T>().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
            return data;
        }
        public async Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool disableTracking = true)
        {
            if (disableTracking)
            {
                return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
            }
            return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<T>> Get(List<int> ids, CancellationToken cancellationToken, bool disableTracking = true)
        {
            if (disableTracking)
            {
                var data = await _dbContext.Set<T>().AsNoTracking().Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
                if (data != null)
                {
                    return data;
                }
                throw new NotFoundException(nameof(T), ids);
            }
            else
            {
                var data = await _dbContext.Set<T>().Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
                if (data != null)
                {
                    return data;
                }
                throw new NotFoundException(nameof(T), ids);
            }

        }

        public async Task<(IList<T>, int)> Get(string Filter, string Order, int? PageNumber, int? PageSize, CancellationToken cancellationToken, bool? disableTracking = true)
        {

            var fopRequest = FopExpressionBuilder<T>.Build(Filter, Order, PageNumber ?? 0, PageSize ?? 0);
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking.HasValue && disableTracking.Value)
            {
                query = query.AsNoTracking();
            }
            var result = query.ApplyFop(fopRequest);
            return (await result.Item1.ToListAsync(cancellationToken), result.Item2);
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            var result = await _dbContext.AddAsync(entity, cancellationToken);
            return result.Entity;
        }


        public async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<T>().Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            return entity != null;
        }

        public async Task Update(T entity, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Set<T>().AsNoTracking().Where(p => p.Id == entity.Id).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                entity.CreateDate = data.CreateDate;
                entity.CreatorID = data.CreatorID;
                entity.RowVersion = data.RowVersion;
                _dbContext.Entry(entity).State = EntityState.Modified;
                //_dbContext.Update(entity);
            }
            else
            {
                throw new NotFoundException(nameof(T), entity.Id);
            }
        }


        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Set<T>().FindAsync(id, cancellationToken);
            if (data != null)
            {
                data.IsDeleted = true;
                _dbContext.Entry(data).State = EntityState.Modified;
                return;
            }
            throw new NotFoundException(nameof(T), id);
        }



    }

}
