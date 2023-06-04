using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.Repositories
{
    public class BoatRepository : GenericRepository<Boat>, IBoatRepository
    {
        public BoatRepository(VehicleProjectContext context) : base(context)
        {
        }
    }
}
