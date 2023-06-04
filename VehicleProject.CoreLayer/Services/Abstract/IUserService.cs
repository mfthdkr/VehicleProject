using Microsoft.AspNetCore.Http.HttpResults;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.DTOs.User;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserDto>> GetUserByNameAsync(string userName);
        Task<Response<NoContent>> CreateUserRoles(string userName);
    }
}
