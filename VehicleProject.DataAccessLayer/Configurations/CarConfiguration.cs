using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.DataAccessLayer.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BrandName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ModelName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NumberOfWheels).IsRequired();
            builder.Property(x => x.SeatCapacity).IsRequired();

            builder.HasOne(x => x.Color)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.ColorId);
        }
    }
}
