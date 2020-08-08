﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.UsersModule;

namespace Theater.Infra.Data.EF.Configurations.UsersModule
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.ID);
            builder.Property(u => u.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
