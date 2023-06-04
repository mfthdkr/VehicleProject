using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.ServiceLayer.Mapping;

namespace VehicleProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : CustomBaseController
    {
        private readonly IBoatService _boatService;
        private readonly IUserService _userService;
        public BoatsController(IBoatService boatService, IUserService userService)
        {
            _boatService = boatService;
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _boatService.GetAllAsync());
        }

        [HttpGet("[action]/{colorId}")]
        public async Task<IActionResult> GetBoatsByColorId(int colorId)
        {
            return ActionResultInstance(await _boatService.Where(b => b.ColorId == colorId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoat([FromBody] BoatCreateDto boatCreateDto)
        {

            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var  dto = ObjectMapper.Mapper.Map<BoatDto>(boatCreateDto);
            dto.UserId = user.Data.Id;

            return ActionResultInstance(await _boatService.AddAsync(dto));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(BoatUpdateDto boatUpdateDto)
        {
            return ActionResultInstance(await _boatService.UpdateByUser(boatUpdateDto, _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.Data.Id));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateHeadlightsStatus(BoatUpdateHeadlightsStatusDto boatUpdateHeadlightsStatusDto)
        {
            return ActionResultInstance(await _boatService.UpdateHeadlightsStatus(boatUpdateHeadlightsStatusDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoat(int id)
        {
            return ActionResultInstance(await _boatService.Remove(id));
        }
        
        

    }
}
