using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamSoft.Domain.Base;

namespace RamSoft.Infrastructure.Configurations.Base
{
    internal class EntityLogDbSetConfiguration : IEntityTypeConfiguration<EntityLog>
    {
        public void Configure(EntityTypeBuilder<EntityLog> builder)
        {
            builder.ToTable("EntityLogDbSet");
            builder.HasKey(p => p.Id).HasName("PK_EntityLogDbSet");
            builder.Property(p => p.Id).UseIdentityColumn();

        }
    }
}
