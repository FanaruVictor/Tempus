using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.Group;

namespace Tempus.Data.Context.Configurations.Group;

public class GroupCategoryConfiguration : IEntityTypeConfiguration<GroupCategory>
{
    public void Configure(EntityTypeBuilder<GroupCategory> builder)
    {
        builder.HasKey(x => new {x.GroupId, x.CategoryId});
        builder
            .HasOne(x => x.Group)
            .WithMany(x => x.GroupCategories)
            .HasForeignKey(x => x.GroupId);
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.GroupCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}