using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.SessionsModule;

namespace Theater.Infra.Data.EF.Configurations.SessionsModule
{
    public class OccupiedChairConfiguration : IEntityTypeConfiguration<OccupiedChair>
    {
        public void Configure(EntityTypeBuilder<OccupiedChair> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Number)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.OccupiedChairs)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Session)
                .WithMany(p => p.OccupiedChairs)
                .HasForeignKey(p => p.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
