using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities.Group;

namespace Tempus.Data.Context.Configurations.Group;

public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.HasKey(x => new { x.GroupId, x.UserId });
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.GroupUsers)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Group)
            .WithMany(x => x.GroupUsers)
            .HasForeignKey(x => x.GroupId);
    }
}