using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface IBoatService : IGenericService<Boat, BoatDto>
    {
        Task<Response<NoDataDto>> UpdateHeadlightsStatus(BoatUpdateHeadlightsStatusDto headlightsStatusDto);
        Task<Response<NoDataDto>> UpdateByUser(BoatUpdateDto boatUpdateDto, string userId);
    }
}
