using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.RoomsModule;

namespace Theater.Infra.Data.EF.Configurations.RoomsModule
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.NumberOfChairs)
                .HasColumnType("int")
                .IsRequired();

            builder.HasMany(p => p.Sessions)
                .WithOne(p => p.Room)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
