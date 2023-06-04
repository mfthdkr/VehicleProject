using VehicleProject.CoreLayer.Entities.Abstract;

namespace VehicleProject.CoreLayer.Entities.Concrete
{
    public class Boat: Vehicle,IBaseEntity
    {   
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
