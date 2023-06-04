using VehicleProject.CoreLayer.Entities.Abstract;

namespace VehicleProject.CoreLayer.Entities.Concrete
{
    public class Car: Vehicle, IBaseEntity
    {
        public int NumberOfWheels { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
