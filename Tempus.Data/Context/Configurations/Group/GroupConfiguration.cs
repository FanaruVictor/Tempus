using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tempus.Data.Context.Configurations.Group;

public class GroupConfiguration : IEntityTypeConfiguration<Core.Entities.Group.Group>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Group.Group> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.OwnerId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany(x => x.GroupUsers)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.HasMany(x => x.GroupCategories)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(u => u.GroupPhoto)
            .WithOne(p => p.Group)
            .HasForeignKey<GroupPhoto>(p => p.GroupId);
    }
}