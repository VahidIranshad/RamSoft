using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;

namespace SharedDatabaseSetup.Jira
{
    internal class TaskBoardStatesSeedData
    {
        internal async static Task SeedData(JiraDbContext context, CancellationToken cancellation)
        {
            var data = context.TaskBoardStatesDbSet.FirstOrDefault(p => p.Id == 0);
            if (data == null)
            {
                var list = new List<TaskBoardStates> {
                new TaskBoardStates{Id = 1, TaskBoardId = 1, StatesId = 1 },
                new TaskBoardStates{Id = 2, TaskBoardId = 1, StatesId = 2 },
                new TaskBoardStates{Id = 3, TaskBoardId = 1, StatesId = 3 }
            };
                context.AddRange(list);
                await context.SaveChangesAsync(null, cancellation);
            }

        }
    }
}
