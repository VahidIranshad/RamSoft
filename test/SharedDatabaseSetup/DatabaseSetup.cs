using RamSoft.Infrastructure.DbContexts;

namespace SharedDatabaseSetup
{
    public class DatabaseSetup
    {
        private static object locker = new object();
        private static object customLocker;
        public static void SeedDataCustomDbContext(JiraDbContext context)
        {
            if (customLocker != null)
            {
                return;
            }
            List<Task> tasks = new List<Task>();
            lock (locker)
            {
                if (customLocker != null)
                {
                    return;
                }

                tasks.Add(Jira.StatesSeedData.SeedData(context, CancellationToken.None));
                tasks.Add(Jira.TaskBoardSeedData.SeedData(context, CancellationToken.None));
                tasks.Add(Jira.TaskBoardStatesSeedData.SeedData(context, CancellationToken.None));
                tasks.Add(Jira.TasksSeedData.SeedData(context, CancellationToken.None));
                Task.WhenAll(tasks);
                customLocker = new object();
            }

        }
    }
}
