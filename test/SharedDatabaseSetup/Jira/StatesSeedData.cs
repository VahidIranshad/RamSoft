using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;

namespace SharedDatabaseSetup.Jira
{
    internal class StatesSeedData
    {
        internal async static Task SeedData(JiraDbContext context, CancellationToken cancellation)
        {
            var data = context.StatesDbSet.FirstOrDefault(p => p.Id == 0);
            if (data == null)
            {
                var list = new List<States> {
                new States{Id = 1, Name = "ToDo"},
                new States{Id = 2, Name = "InProgress"},
                new States{Id = 3, Name = "Review"},
                new States{Id = 4, Name = "Done"},
                new States{Id = 5, Name = "xxx", IsDeleted = false},
            };
                context.AddRange(list);
                await context.SaveChangesAsync(null, cancellation);
            }

        }
    }
}
