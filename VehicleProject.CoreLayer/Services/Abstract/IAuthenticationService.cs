using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.DTOs.User;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);

        Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);

    }
}
