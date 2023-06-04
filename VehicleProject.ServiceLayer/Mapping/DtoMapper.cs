using AutoMapper;
using VehicleProject.CoreLayer.DTOs.Boat;
using VehicleProject.CoreLayer.DTOs.Bus;
using VehicleProject.CoreLayer.DTOs.Car;
using VehicleProject.CoreLayer.DTOs.Color;
using VehicleProject.CoreLayer.DTOs.User;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.ServiceLayer.Mapping
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserDto, User>().ReverseMap();


            CreateMap<BoatDto, Boat>().ReverseMap();
            CreateMap<BoatCreateDto, BoatDto>().ReverseMap();
            CreateMap<BoatCreateDto, Boat>().ReverseMap();
            CreateMap<BoatUpdateDto, Boat>().ReverseMap();


            CreateMap<BusDto, Bus>().ReverseMap();
            CreateMap<BusCreateDto, BusDto>().ReverseMap();
            CreateMap<BusCreateDto, Bus>().ReverseMap();

            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CarCreateDto, CarDto>().ReverseMap();
            CreateMap<CarCreateDto, Car>().ReverseMap();

            CreateMap<ColorDto, Color>().ReverseMap();
            CreateMap<ColorCreateDto, Color>().ReverseMap();
            CreateMap<ColorUpdateDto, Color>().ReverseMap();
            CreateMap<ColorCreateDto, ColorDto>().ReverseMap();
            CreateMap<ColorUpdateDto, ColorDto>().ReverseMap();
            
        }
    }
}
