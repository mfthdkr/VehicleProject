using VehicleProject.CoreLayer.DTOs.Common;

namespace VehicleProject.CoreLayer.DTOs.Bus
{
    public class BusDto: VehicleBaseDto
    {
        public int NumberOfWheels { get; set; }
        public int ColorId { get; set; }
        public string UserId { get; set; }
    }
}
