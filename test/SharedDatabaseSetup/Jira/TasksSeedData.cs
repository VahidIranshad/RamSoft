using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;

namespace SharedDatabaseSetup.Jira
{
    internal class TasksSeedData
    {
        internal async static Task SeedData(JiraDbContext context, CancellationToken cancellation)
        {
            var data = context.TasksDbSet.FirstOrDefault(p => p.Id == 0);
            if (data == null)
            {
                var list = new List<Tasks> {
                    new Tasks{Id = 1, Name = "new task", Description = "Description", TaskBoardId = 1, StatesId = 2, Deadline = DateTime.Now }

            };
                context.AddRange(list);
                await context.SaveChangesAsync(null, cancellation);
            }

        }
    }
}
