using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.CoreLayer.DTOs.Color;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.ServiceLayer.Mapping;

namespace VehicleProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : CustomBaseController
    {
        private readonly IGenericService<Color, ColorDto> _colorService;

        public ColorsController(IGenericService<Color, ColorDto> colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _colorService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(ColorCreateDto colorCreateDto)
        {
            return ActionResultInstance(await _colorService.AddAsync(ObjectMapper.Mapper.Map<ColorDto>(colorCreateDto)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateColor(ColorUpdateDto colorUpdateDto)
        {
            return ActionResultInstance(await _colorService.Update(ObjectMapper.Mapper.Map<ColorDto>(colorUpdateDto), colorUpdateDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            return ActionResultInstance(await _colorService.Remove(id));
        }
    }
}
