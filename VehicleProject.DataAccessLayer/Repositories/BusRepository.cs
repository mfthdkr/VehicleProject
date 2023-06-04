using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.Repositories
{
    public class BusRepository : GenericRepository<Bus>, IBusRepository
    {
        public BusRepository(VehicleProjectContext context) : base(context)
        {
        }
    }
}
