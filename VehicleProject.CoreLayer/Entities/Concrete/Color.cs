using VehicleProject.CoreLayer.Entities.Abstract;

namespace VehicleProject.CoreLayer.Entities.Concrete
{
    public class Color : IBaseEntity
    {
        public Color()
        {
            Buses = new HashSet<Bus>();
            Cars = new HashSet<Car>();
            Boats = new HashSet<Boat>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;

        public string Name { get; set; }

        public virtual ICollection<Bus> Buses { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Boat> Boats { get; set; }
    }
}
