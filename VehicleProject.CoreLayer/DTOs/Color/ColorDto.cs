using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Car;
using VehicleProject.CoreLayer.DTOs.Common;

namespace VehicleProject.CoreLayer.DTOs.Color
{
    public class ColorDto : BaseDto
    {
        public string Name { get; set; }

        public List<BusDto> Buses { get; set; }
        public List<CarDto> Cars { get; set; }
        public List<BoatDto> Boats { get; set; }
    }
}
