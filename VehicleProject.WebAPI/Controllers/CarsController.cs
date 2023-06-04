using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Car;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.ServiceLayer.Mapping;
using VehicleProject.ServiceLayer.Services;

namespace VehicleProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : CustomBaseController
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        public CarsController(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _carService.GetAllAsync());
        }

        [HttpGet("[action]/{colorId}")]
        public async Task<IActionResult> GetCarsByColorId(int colorId)
        {
            return ActionResultInstance(await _carService.Where(b => b.ColorId == colorId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto carCreateDto)
        {

            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var dto = ObjectMapper.Mapper.Map<CarDto>(carCreateDto);
            dto.UserId = user.Data.Id;

            return ActionResultInstance(await _carService.AddAsync(dto));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(CarUpdateDto carUpdateDto)
        {
            return ActionResultInstance(await _carService.UpdateByUser(carUpdateDto, _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.Data.Id));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateHeadlightsStatus(CarUpdateHeadlightsStatusDto boatUpdateHeadlightsStatusDto)
        {
            return ActionResultInstance(await _carService.UpdateHeadlightsStatus(boatUpdateHeadlightsStatusDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            return ActionResultInstance(await _carService.Remove(id));
        }
    }
}
