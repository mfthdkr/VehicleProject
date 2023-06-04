using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VehicleProject.CoreLayer.Configurations;
using VehicleProject.CoreLayer.DTOs.User;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Services;
using VehicleProject.CoreLayer.Services.Abstract;

namespace VehicleProject.ServiceLayer.Services
{
    // Dışarı açılmıyacak kendi iç yapımızda token işlemleri için kullanılacak.
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<User> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        /// <summary>
        /// Refresh Token oluşturur.
        /// </summary>
        /// <returns></returns>
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        /// <summary>
        /// Token Payload(data)'ında olacak claim listesini oluşturur.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="audiences"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Claim>> GetClaims(User user, List<string> audiences)
        {   
            // Kullanıcı rolleri
            var userRoles = await _userManager.GetRolesAsync(user);

            // Claim listesi
            var userList = new List<Claim> {
            // Id
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            //  Email
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            // UserName
            new Claim(ClaimTypes.Name,user.UserName),
            // Her token için random Id
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            // audience'lerin her birini claim nesnesi olarak ekledik.
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            // Rolleri claim olarak ekledik.
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            return userList;
        }

        /// <summary>
        ///  Token oluşturur.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenDto CreateToken(User user)
        {
            // token ömrü
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            // refresh token ömrü
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            // tokenı imzalayacak olan key
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            // imzalama 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(user, _tokenOption.Audience).Result,
                 signingCredentials: signingCredentials);

            // handler, token oluşturur.
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            
            // token'ı Dto'ya dönüştürdük.
            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }

    }
}
