using Microsoft.AspNetCore.Http;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;

namespace VehicleProject.ServiceLayer.Services
{
    public class BusService : GenericService<Bus, BusDto>, IBusService
    {
        public BusService(IUnitOfWork unitOfWork, IGenericRepository<Bus> genericRepository) : base(unitOfWork, genericRepository)
        {
        }

        public async Task<Response<NoDataDto>> UpdateHeadlightsStatus(BusUpdateHeadlightsStatusDto headlightsStatusDto)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(headlightsStatusDto.Id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            isExistEntity.IsHeadlightOn = headlightsStatusDto.IsHeadlightOn;

            _genericRepository.Update(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }


        public async Task<Response<NoDataDto>> UpdateByUser(BusUpdateDto busUpdateDto, string userId)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(busUpdateDto.Id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            isExistEntity.BrandName = busUpdateDto.BrandName;
            isExistEntity.ModelName = busUpdateDto.ModelName;
            isExistEntity.SeatCapacity = busUpdateDto.SeatCapacity;
            isExistEntity.IsHeadlightOn = busUpdateDto.IsHeadlightOn;
            isExistEntity.ColorId = busUpdateDto.ColorId;
            isExistEntity.NumberOfWheels = busUpdateDto.NumberOfWheels;

            _genericRepository.Update(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
