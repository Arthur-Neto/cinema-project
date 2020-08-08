using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.RoomsModule;

namespace Theater.Infra.Data.EF.Configurations.RoomsModule
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(u => u.ID);
            builder.Property(u => u.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.NumberOfChairs)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
