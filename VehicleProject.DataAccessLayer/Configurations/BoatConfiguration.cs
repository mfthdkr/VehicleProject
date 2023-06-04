using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.DataAccessLayer.Configurations
{
    public class BoatConfiguration : IEntityTypeConfiguration<Boat>
    {
        public void Configure(EntityTypeBuilder<Boat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BrandName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ModelName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SeatCapacity).IsRequired();

            builder.HasOne(x => x.Color)
                .WithMany(x => x.Boats)
                .HasForeignKey(x => x.ColorId);
        }
    }
}
