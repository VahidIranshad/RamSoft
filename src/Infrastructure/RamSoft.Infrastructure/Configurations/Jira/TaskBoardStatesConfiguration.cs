using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamSoft.Domain.Jira;

namespace RamSoft.Infrastructure.Configurations.Jira
{
    internal class TaskBoardStatesConfiguration : IEntityTypeConfiguration<TaskBoardStates>
    {
        public void Configure(EntityTypeBuilder<TaskBoardStates> builder)
        {
            builder.ToTable("TaskBoardStates", "Jira");
            builder.HasKey(p => p.Id).HasName("PK_Jira_TaskBoardStates");
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.OrderShow).IsRequired();

            builder.HasOne(d => d.States)
                .WithMany()
                .HasForeignKey(d => d.StatesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.TaskBoard)
                .WithMany(d => d.TaskBoardStateList)
                .HasForeignKey(d => d.TaskBoardId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.RowVersion).HasColumnType("RowVersion").IsRowVersion();

            /*base my exprience I inclueded author automatically*/
            builder.Navigation(e => e.States).AutoInclude();
            builder.Navigation(e => e.TaskBoard).AutoInclude();

        }
    }
}
