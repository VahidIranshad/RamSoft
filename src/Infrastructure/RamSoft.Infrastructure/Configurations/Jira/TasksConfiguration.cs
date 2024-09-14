using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamSoft.Domain.Jira;

namespace RamSoft.Infrastructure.Configurations.Jira
{
    internal class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("Tasks", "Jira");
            builder.HasKey(p => p.Id).HasName("PK_Jira_Tasks");
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar");
            builder.Property(p => p.Deadline).IsRequired();
            builder.Property(p => p.Id).UseIdentityColumn();

            builder.HasOne(d => d.States)
                .WithMany()
                .HasForeignKey(d => d.StatesId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(d => d.TaskBoard)
                .WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskBoardId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Property<byte[]>("Version").IsRowVersion();

            /*base my exprience I inclueded author automatically*/
            builder.Navigation(e => e.States).AutoInclude();
            builder.Navigation(e => e.TaskBoard).AutoInclude();

        }
    }
    }
