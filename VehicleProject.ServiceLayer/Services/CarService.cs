using Microsoft.AspNetCore.Http;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Car;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;

namespace VehicleProject.ServiceLayer.Services
{
    public class CarService : GenericService<Car, CarDto>,ICarService
    {
        public CarService(IUnitOfWork unitOfWork, IGenericRepository<Car> genericRepository) : base(unitOfWork, genericRepository)
        {
        }

        public async Task<Response<NoDataDto>> UpdateHeadlightsStatus(CarUpdateHeadlightsStatusDto headlightsStatusDto)
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


        public async Task<Response<NoDataDto>> UpdateByUser(CarUpdateDto carUpdateDto, string userId)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(carUpdateDto.Id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            isExistEntity.BrandName = carUpdateDto.BrandName;
            isExistEntity.ModelName = carUpdateDto.ModelName;
            isExistEntity.SeatCapacity = carUpdateDto.SeatCapacity;
            isExistEntity.IsHeadlightOn = carUpdateDto.IsHeadlightOn;
            isExistEntity.ColorId = carUpdateDto.ColorId;
            isExistEntity.NumberOfWheels = carUpdateDto.NumberOfWheels;

            _genericRepository.Update(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
