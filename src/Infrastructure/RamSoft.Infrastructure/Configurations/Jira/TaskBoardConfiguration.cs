using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamSoft.Domain.Jira;

namespace RamSoft.Infrastructure.Configurations.Jira
{
    internal class TaskBoardConfiguration : IEntityTypeConfiguration<TaskBoard>
    {
        public void Configure(EntityTypeBuilder<TaskBoard> builder)
        {
            builder.ToTable("TaskBoard", "Jira");
            builder.HasKey(p => p.Id).HasName("PK_Jira_TaskBoard");
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

            builder.HasOne(d => d.DefaultStates)
                .WithMany()
                .HasForeignKey(d => d.DefaultStatesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property<byte[]>("Version").IsRowVersion();

            /*base my exprience I inclueded author automatically*/
            builder.Navigation(e => e.DefaultStates).AutoInclude();
            builder.Navigation(e => e.TaskBoardStateList).AutoInclude();
            builder.Navigation(e => e.Tasks).AutoInclude();

        }
    }
}
