using Microsoft.EntityFrameworkCore;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Infrastructure.Repositories.Base;

namespace RamSoft.Infrastructure.Repositories.Jira
{
    public class TaskBoardStatesRepository : GenericRepository<TaskBoardStates>, ITaskBoardStatesRepository
    {
        public TaskBoardStatesRepository(JiraDbContext dbContext) :
            base(dbContext)
        {
        }

        public async Task<bool> Exists(int taskBoardId, int statesId, CancellationToken cancellationToken)
        {
            return await _dbContext.TaskBoardStatesDbSet.Where(p => p.StatesId == statesId && p.TaskBoardId == taskBoardId).AnyAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TaskBoardStates>> GetListByTaskBoardId(int taskBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.TaskBoardStatesDbSet.Where(p => p.TaskBoardId == taskBoardId).ToListAsync(cancellationToken);
        }
    }
}
