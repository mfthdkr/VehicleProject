using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.DTOs.User;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.ServiceLayer.Mapping;

namespace VehicleProject.ServiceLayer.Services
{   
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Kullanıcı oluşturur.
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns></returns>
        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName };

            // Identity UserManager<> üzerinden ekleme yaptığımız için Password hashlemesini kendi yapar.
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserDto>.Fail(new ErrorDto(errors, true), StatusCodes.Status400BadRequest);
            }
            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), StatusCodes.Status200OK);
        }


        /// <summary>
        /// Kullanıcının bilgilerini getirir.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserDto>.Fail("UserName not found", StatusCodes.Status404NotFound, true);
            }

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), StatusCodes.Status200OK);
        }
    
        /// <summary>
        /// Kullanıcıya rol atama işlemi yapar. Öncesinde Role tablosu boş ise admin ve manager değerlerini atar.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Response<NoContent>> CreateUserRoles(string userName)
        {

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
                await _roleManager.CreateAsync(new() { Name = "manager" });
            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "manager");


            return Response<NoContent>.Success(StatusCodes.Status201Created);

        }

    }
}
