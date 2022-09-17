using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public sealed class PoolOptionsConfiguration : IEntityTypeConfiguration<PoolOptions>
{
    public void Configure(EntityTypeBuilder<PoolOptions> builder)
    {
        builder.Property(o => o.MaximumCap)
            .HasPrecision(11, 2);

        builder.HasOne(o => o.Pool).WithOne(p => p.Options).HasForeignKey<PoolOptions>(o => o.PoolId).HasConstraintName("PoolOptions_Pool_FK1");
    }
}
