using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface IBusService : IGenericService<Bus, BusDto>
    {
        Task<Response<NoDataDto>> UpdateHeadlightsStatus(BusUpdateHeadlightsStatusDto headlightsStatusDto);
        Task<Response<NoDataDto>> UpdateByUser(BusUpdateDto busUpdateDto, string userId);
    }
}
