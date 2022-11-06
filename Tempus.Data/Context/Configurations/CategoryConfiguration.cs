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
	}
}