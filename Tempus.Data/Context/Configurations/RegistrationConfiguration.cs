using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities;

namespace Tempus.Data.Context.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
	public void Configure(EntityTypeBuilder<Registration> builder)
	{
		builder.Property(x => x.Title).IsRequired();
		builder.Property(x => x.Content).IsRequired();
		builder.Property(x => x.CreatedAt).IsRequired();
		builder.Property(x => x.LastUpdatedAt).IsRequired();

		builder
			.HasOne(x => x.User)
			.WithMany(x => x.Registrations)
			.HasForeignKey(x => x.UserId);
		builder
			.HasOne(x => x.Category)
			.WithMany(x => x.Registrations)
			.HasForeignKey(x => x.CategoryId);
	}
}