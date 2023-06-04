using VehicleProject.CoreLayer.DTOs.Color;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;

namespace VehicleProject.ServiceLayer.Services
{
    public class ColorService : GenericService<Color, ColorDto>,IColorService
    {
        public ColorService(IUnitOfWork unitOfWork, IGenericRepository<Color> genericRepository) : base(unitOfWork, genericRepository)
        {
        }
    }
}
