using VehicleProject.CoreLayer.DTOs.Common;

namespace VehicleProject.CoreLayer.DTOs.Car
{
    public class CarDto: VehicleBaseDto
    {
        public int NumberOfWheels { get; set; }
        public int ColorId { get; set; }
        public string UserId { get; set; }
    }
}
