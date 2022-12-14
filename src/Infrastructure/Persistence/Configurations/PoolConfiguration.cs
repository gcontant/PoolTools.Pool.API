using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class PoolConfiguration : IEntityTypeConfiguration<Pool>
{
    public void Configure(EntityTypeBuilder<Pool> builder)
    {        
        builder.Property(p => p.Id).HasDefaultValueSql($"NEXT VALUE FOR {Constants.PoolSequenceName}");

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(p => p.Teams).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}
