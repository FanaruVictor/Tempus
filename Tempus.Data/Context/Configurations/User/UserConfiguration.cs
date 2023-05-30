using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.User;

namespace Tempus.Data.Context.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User.User>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User.User> builder)
    {
        builder.Property(x => x.Id).HasMaxLength(36).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.IsDarkTheme).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.ExternalId).IsRequired(false);
        builder
            .HasOne(u => u.UserPhoto)
            .WithOne(p => p.User)
            .HasForeignKey<UserPhoto>(p => p.UserId);
        
        builder.HasMany(x => x.GroupUsers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.UserCategories)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        
    }
}