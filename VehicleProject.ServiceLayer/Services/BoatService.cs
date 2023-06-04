using Microsoft.AspNetCore.Http;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;

namespace VehicleProject.ServiceLayer.Services
{
    public class BoatService : GenericService<Boat, BoatDto>, IBoatService
    {   
        public BoatService(IUnitOfWork unitOfWork, IGenericRepository<Boat> genericRepository) : base(unitOfWork, genericRepository)
        {
        }

        public async Task<Response<NoDataDto>> UpdateHeadlightsStatus(BoatUpdateHeadlightsStatusDto headlightsStatusDto)
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

        public async Task<Response<NoDataDto>> UpdateByUser(BoatUpdateDto boatUpdateDto, string userId)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(boatUpdateDto.Id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            isExistEntity.BrandName = boatUpdateDto.BrandName;
            isExistEntity.ModelName = boatUpdateDto.ModelName;
            isExistEntity.SeatCapacity = boatUpdateDto.SeatCapacity;
            isExistEntity.IsHeadlightOn = boatUpdateDto.IsHeadlightOn;
            isExistEntity.ColorId = boatUpdateDto.ColorId;

            _genericRepository.Update(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
