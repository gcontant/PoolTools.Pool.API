using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class PoolTeamConfiguration : IEntityTypeConfiguration<PoolTeam>
{
    public void Configure(EntityTypeBuilder<PoolTeam> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasDefaultValueSql($"NEXT VALUE FOR {Constants.PoolTeamSequenceName}");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Owner)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsMany(t => t.DraftPicks);
    }
}
