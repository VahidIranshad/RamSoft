using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Infrastructure.Repositories.Base;

namespace RamSoft.Infrastructure.Repositories.Jira
{
    public class TaskBoardRepository : GenericRepository<TaskBoard>, ITaskBoardRepository
    {
        public TaskBoardRepository(JiraDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
