﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Core.Entities;

namespace Tempus.Data.Context.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.Property(x => x.Id).HasMaxLength(36).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.LastUpdatedAt).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Registrations)
            .HasForeignKey(x => x.CategoryId);
    }
}