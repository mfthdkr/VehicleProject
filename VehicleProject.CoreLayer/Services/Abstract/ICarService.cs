using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Car;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface ICarService : IGenericService<Car, CarDto>
    {
        Task<Response<NoDataDto>> UpdateHeadlightsStatus(CarUpdateHeadlightsStatusDto headlightsStatusDto);
        Task<Response<NoDataDto>> UpdateByUser(CarUpdateDto carUpdateDto, string userId);
    }
}
