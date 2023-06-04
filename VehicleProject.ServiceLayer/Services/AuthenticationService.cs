using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.DTOs.User;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;

namespace VehicleProject.ServiceLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        /// <summary>
        /// Kullanıcı girişi ile JWT token üretir.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Response<TokenDto>
                    .Fail("Email or Password is wrong", StatusCodes.Status400BadRequest, true);

            // password doğruluğunu kontrol eder. 
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Response<TokenDto>
                    .Fail("Email or Password is wrong", StatusCodes.Status400BadRequest, true);
            }

            // access token üretir.
            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService
                .Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            // Kullanıcı için daha önce UserRefreshToken oluşturulmamışsa UserRefreshToken oluşturur. Oluşturulmuşsa ömrünü yenile.
            if (userRefreshToken == null)
            {
                await _userRefreshTokenService
                    .AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(token, StatusCodes.Status200OK);
        }

        /// <summary>
        /// Refresh token ile JWT token üretir.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", StatusCodes.Status404NotFound, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(tokenDto, StatusCodes.Status200OK);
        }

        /// <summary>
        /// Kullanıcı çıkış/logut yaparsa refresh token'ı siler.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService
                .Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);
            }

            _userRefreshTokenService.HardRemove(existRefreshToken);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status200OK);
        }
    }
}
