using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.User;

namespace Tempus.Data.Context.Configurations.User;

public class UserCategoryConfiguration : IEntityTypeConfiguration<UserCategory>
{
    public void Configure(EntityTypeBuilder<UserCategory> builder)
    {
        builder.HasKey(x => new {x.UserId, x.CategoryId});
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserCategories)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.UserCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}