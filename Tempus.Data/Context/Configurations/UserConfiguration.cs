using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities;

namespace Tempus.Data.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(x => x.Id).HasMaxLength(36).IsRequired();
		builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
		builder.Property(x => x.Email).IsRequired();
	}
}