using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(VehicleProjectContext context) : base(context)
        {
        }
    }
}
