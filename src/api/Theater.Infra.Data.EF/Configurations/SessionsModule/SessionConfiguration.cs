using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.SessionsModule;

namespace Theater.Infra.Data.EF.Configurations.SessionsModule
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Date)
                .HasColumnType("datetimeoffset")
                .IsRequired();

            builder.HasOne(p => p.Movie)
                .WithMany(p => p.Sessions)
                .HasForeignKey(p => p.MovieId);

            builder.HasOne(p => p.Room)
                .WithMany(p => p.Sessions)
                .HasForeignKey(p => p.RoomId);
        }
    }
}
