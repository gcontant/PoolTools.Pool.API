using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR { Constants.PlayerSequenceName}");

        builder.Property(p => p.FullName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.AAV)
            .HasPrecision(10,2);

        builder.OwnsOne(p => p.Position,
            sa =>
            {   
                sa.Property(p => p.Code)
                    .HasColumnName("Position")
                    .HasMaxLength(2)
                    .IsRequired();

                sa.Ignore(p => p.IsForward);
            });
        builder.OwnsOne(p => p.Team,
            sa =>
            {
                sa.Property(t => t.Code)
                    .HasColumnName("Team")
                    .HasMaxLength(3)
                    .IsRequired();
            });
    }
}
