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

        builder.OwnsOne(p => p.Position);
        builder.OwnsOne(p => p.Team);
    }
}
