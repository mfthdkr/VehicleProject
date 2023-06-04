using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(VehicleProjectContext context) : base(context)
        {
        }
    }
}
