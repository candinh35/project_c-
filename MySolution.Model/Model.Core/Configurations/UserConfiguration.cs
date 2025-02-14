using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Core.Entities;

namespace Model.Core.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "public");

        builder.Property(x => x.Code).HasMaxLength(15);
        builder.Property(x => x.HashPass).HasMaxLength(100);

        builder
            .HasMany(x => x.RefreshTokens)
            .WithOne(y => y.User)
            .HasPrincipalKey(x => x.id)
            .HasForeignKey(x => x.UserId);

    }
}