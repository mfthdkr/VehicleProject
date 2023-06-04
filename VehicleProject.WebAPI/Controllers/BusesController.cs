using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.ServiceLayer.Mapping;
using VehicleProject.ServiceLayer.Services;

namespace VehicleProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : CustomBaseController
    {
        private readonly IBusService _busService;
        private readonly IUserService _userService;
        public BusesController(IBusService busService, IUserService userService)
        {
            _busService = busService;
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _busService.GetAllAsync());
        }

        [HttpGet("[action]/{colorId}")]
        public async Task<IActionResult> GetBusesByColorId(int colorId)
        {
            return ActionResultInstance(await _busService.Where(b => b.ColorId == colorId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBus([FromBody] BusCreateDto busCreateDto)
        {

            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var dto = ObjectMapper.Mapper.Map<BusDto>(busCreateDto);
            dto.UserId = user.Data.Id;

            return ActionResultInstance(await _busService.AddAsync(dto));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(BusUpdateDto busUpdateDto)
        {
            return ActionResultInstance(await _busService.UpdateByUser(busUpdateDto, _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.Data.Id));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateHeadlightsStatus(BusUpdateHeadlightsStatusDto boatUpdateHeadlightsStatusDto)
        {
            return ActionResultInstance(await _busService.UpdateHeadlightsStatus(boatUpdateHeadlightsStatusDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoat(int id)
        {
            return ActionResultInstance(await _busService.Remove(id));
        }

    }
}
