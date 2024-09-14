using Microsoft.EntityFrameworkCore;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Infrastructure.Repositories.Base;

namespace RamSoft.Infrastructure.Repositories.Jira
{
    public class TasksRepository : GenericRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(JiraDbContext dbContext) :
            base(dbContext)
        {
        }

        public async Task<bool> Exists(int taskBoardId, int statesId, CancellationToken cancellationToken)
        {
            return await _dbContext.TasksDbSet.Where(p => p.StatesId == statesId && p.TaskBoardId == taskBoardId).AnyAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Tasks>> GetListByTaskBoardId(int taskBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.TasksDbSet.Where(p => p.TaskBoardId == taskBoardId).ToListAsync(cancellationToken);
        }
    }
}
