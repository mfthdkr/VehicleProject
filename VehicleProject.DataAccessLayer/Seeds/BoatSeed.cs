using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.DataAccessLayer.Seeds
{
    public class BoatSeed : IEntityTypeConfiguration<Boat>
    {
        public void Configure(EntityTypeBuilder<Boat> builder)
        {
            builder.HasData(
                new Boat { Id=1,UserId= "b2f65c29-f3d5-4244-a648-d194869a3c9b",ColorId=1 ,BrandName="BrandName 1",ModelName="ModelName 1"},
                new Boat { Id=2,UserId= "b2f65c29-f3d5-4244-a648-d194869a3c9b",ColorId=2, BrandName = "BrandName 2", ModelName = "ModelName 2" },
                new Boat { Id=3,UserId= "b2f65c29-f3d5-4244-a648-d194869a3c9b",ColorId=3, BrandName = "BrandName 3", ModelName = "ModelName 3" }
                );
        }
    }
}
