using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;

namespace SharedDatabaseSetup.Jira
{
    internal class TaskBoardSeedData
    {
        internal async static Task SeedData(JiraDbContext context, CancellationToken cancellation)
        {
            var data = context.TaskBoardDbSet.FirstOrDefault(p => p.Id == 0);
            if (data == null)
            {
                var list = new List<TaskBoard> {
                new TaskBoard{Id = 1, Name = "ToDo", DefaultStatesId = 1 }
            };
                context.AddRange(list);
                await context.SaveChangesAsync(null, cancellation);
            }

        }
    }
}
