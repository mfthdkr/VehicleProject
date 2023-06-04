using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.DataAccessLayer.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               // Password= 123456aZ
               new User { Id = "b2f65c29-f3d5-4244-a648-d194869a3c9b", Email = "mfthdkr@gmail.com", NormalizedEmail = "MFTHDKR@GMAIL.COM", UserName = "mfthdkr", NormalizedUserName = "MFTHDKR", FirstName = "Fatih", LastName = "Diker", PasswordHash = "AQAAAAIAAYagAAAAEPiv9gbc9X0DiofTY4RAxE7zt/i3u43JYovTXD08jdSMgrViZZEy9AAdh14cEt3zUg==" }
                );
        }
    }
}
