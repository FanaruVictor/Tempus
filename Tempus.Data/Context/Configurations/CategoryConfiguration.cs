using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities;

namespace Tempus.Data.Context.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Color).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.LastUpdatedAt).IsRequired();
        builder.HasMany(x => x.Registrations).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.UserCategories)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.GroupCategories)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}