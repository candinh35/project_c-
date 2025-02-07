using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Core.Entities;

namespace Model.Core.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "public");

        builder.Property(x => x.Name).HasMaxLength(15);
        builder.Property(x => x.Email).HasMaxLength(15);
    }
}