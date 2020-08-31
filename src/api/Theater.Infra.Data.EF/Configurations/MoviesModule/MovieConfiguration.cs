using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Domain.MoviesModule;

namespace Theater.Infra.Data.EF.Configurations.MoviesModule
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(u => u.ID);
            builder.Property(u => u.ID)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.ImagePath)
                .HasColumnType("varchar")
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            builder.Property(u => u.Title)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(u => u.Duration)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.ScreenType)
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(u => u.AudioType)
                .HasColumnType("smallint")
                .IsRequired();

            builder.HasMany(p => p.Sessions)
                .WithOne(p => p.Movie)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
