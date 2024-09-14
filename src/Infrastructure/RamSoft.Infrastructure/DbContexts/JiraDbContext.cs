using Microsoft.EntityFrameworkCore;
using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Base;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.Extensions;

namespace RamSoft.Infrastructure.DbContexts
{
    public class JiraDbContext : DbContext
    {
        public JiraDbContext(DbContextOptions<JiraDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JiraDbContext).Assembly);
            modelBuilder.ApplySoftDeleteQueryFilter();
        }

        public virtual async Task<int> SaveChangesAsync(ICurrentUserService user, CancellationToken cancellationToken)
        {
            try
            {
                var createList = new List<BaseEntity>();
                var logs = new List<EntityLog>();

                foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                    .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified || q.State == EntityState.Deleted))
                {
                    var entityLog = new EntityLog
                    {
                        LastEditDate = DateTime.Now,
                        CreateDate = DateTime.Now,
                        CreatorID = user == null ? "Admin" : user.UserId,
                        LastEditorID = user == null ? "Admin" : user.UserId,
                        ClassName = entry.Entity.GetType().Name,
                        Key = entry.Entity.Id.ToString()

                    };
                    if (entry.State == EntityState.Deleted)
                    {
                        entityLog.ChangeType = "D";
                        logs.Add(entityLog);
                    }
                    else
                    {
                        entry.Entity.LastEditDate = DateTime.Now;
                        entry.Entity.LastEditorID = user == null ? "Admin" : user.UserId;

                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.CreateDate = DateTime.Now;
                            entry.Entity.CreatorID = user == null ? "Admin" : user.UserId;

                            createList.Add(entry.Entity);
                        }
                        else
                        {
                            entityLog.ChangeType = "U";
                            logs.Add(entityLog);
                        }
                    }
                }

                var result = await base.SaveChangesAsync();

                foreach (var item in createList)
                {
                    var entityLog = new EntityLog
                    {
                        LastEditDate = DateTime.Now,
                        CreateDate = DateTime.Now,
                        CreatorID = user == null ? "Admin" : user.UserId,
                        LastEditorID = user == null ? "Admin" : user.UserId,
                        ClassName = item.GetType().Name,
                        Key = item.Id.ToString()

                    };
                    entityLog.ChangeType = "I";
                    logs.Add(entityLog);
                }

                this.EntityLogDbSet.AddRange(logs);


                return result;
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        public DbSet<EntityLog> EntityLogDbSet { get; set; }
        public DbSet<States> StatesDbSet { get; set; }
        public DbSet<TaskBoard> TaskBoardDbSet { get; set; }
        public DbSet<TaskBoardStates> TaskBoardStatesDbSet { get; set; }
        public DbSet<Tasks> TasksDbSet { get; set; }
    }
}
