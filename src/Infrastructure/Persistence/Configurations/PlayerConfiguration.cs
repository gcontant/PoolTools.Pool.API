using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FullName)
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(p => p.Position,
            sa =>
            {
                sa.Property(p => p.Code).HasColumnName("Position");
                sa.Ignore(p => p.IsForward);
            });
        builder.OwnsOne(p => p.Team,
            sa =>
            {
                sa.Property(t => t.Code).HasColumnName("Team");
            });
    }
}
