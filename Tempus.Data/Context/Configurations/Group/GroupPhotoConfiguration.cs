using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tempus.Data.Context.Configurations.Group;

public class GroupPhotoConfiguration: IEntityTypeConfiguration<GroupPhoto>
{
    public void Configure(EntityTypeBuilder<GroupPhoto> builder)
    {
        builder.Property(x => x.PublicId).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder
            .HasOne(u => u.Group)
            .WithOne(p => p.GroupPhoto)
            .HasForeignKey<GroupPhoto>(p => p.GroupId);     
    }
}