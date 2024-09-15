using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamSoft.Domain.Jira;

namespace RamSoft.Infrastructure.Configurations.Jira
{
    internal class StatesConfiguration : IEntityTypeConfiguration<States>
    {
        public void Configure(EntityTypeBuilder<States> builder)
        {
            builder.ToTable("States", "Jira");
            builder.HasKey(p => p.Id).HasName("PK_Jira_States");
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(x => x.RowVersion).HasColumnType("RowVersion").IsRowVersion();
            builder.HasData(
                new States
                {
                    Id = 1,
                    Name = "To Do",
                    CreatorID = "Admin",
                    LastEditorID = "Admin",
                    CreateDate = new DateTime(2024, 09, 13),
                    LastEditDate = new DateTime(2024, 09, 13)
                },
                new States
                {
                    Id = 2,
                    Name = "In Progress",
                    CreatorID = "Admin",
                    LastEditorID = "Admin",
                    CreateDate = new DateTime(2024, 09, 13),
                    LastEditDate = new DateTime(2024, 09, 13)
                },
                new States
                {
                    Id = 3,
                    Name = "Review",
                    CreatorID = "Admin",
                    LastEditorID = "Admin",
                    CreateDate = new DateTime(2024, 09, 13),
                    LastEditDate = new DateTime(2024, 09, 13)
                },
                new States
                {
                    Id = 4,
                    Name = "Done",
                    CreatorID = "Admin",
                    LastEditorID = "Admin",
                    CreateDate = new DateTime(2024, 09, 13),
                    LastEditDate = new DateTime(2024, 09, 13)
                }
           );
        }
    }
}
