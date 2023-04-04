using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.User;

namespace Tempus.Data.Context.Configurations.User;

public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.Property(x => x.PublicId).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder
            .HasOne(u => u.User)
            .WithOne(p => p.UserPhoto)
            .HasForeignKey<UserPhoto>(p => p.UserId);        
    }
}
