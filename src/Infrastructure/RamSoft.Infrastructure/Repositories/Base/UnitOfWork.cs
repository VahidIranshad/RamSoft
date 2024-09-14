using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Base;
using RamSoft.Infrastructure.DbContexts;
using System.Collections;

namespace RamSoft.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly JiraDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private Hashtable _repositories;

        public IStatesRepository StatesRepository { get; private set; }

        public ITaskBoardRepository TaskBoardRepository { get; private set; }

        public ITaskBoardStatesRepository TaskBoardStatesRepository { get; private set; }

        public ITasksRepository TasksRepository { get; private set; }

        public UnitOfWork(
            JiraDbContext context
            , ICurrentUserService currentUserService
            , IStatesRepository statesRepository
            , ITaskBoardRepository taskBoardRepository
            , ITaskBoardStatesRepository taskBoardStatesRepository
            , ITasksRepository tasksRepository)
        {
            _context = context;
            this._currentUserService = currentUserService;
            StatesRepository = statesRepository;
            TaskBoardRepository = taskBoardRepository;
            TaskBoardStatesRepository = taskBoardStatesRepository;
            TasksRepository = tasksRepository;
        }


        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            foreach (var cacheKey in cacheKeys)
            {
                //_cache.Remove(cacheKey);
            }
            return result;
        }

        public Task Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(_currentUserService, cancellationToken);
        }

    }
}
