using VehicleProject.CoreLayer.DTOs.User;
using VehicleProject.CoreLayer.Entities.Concrete;

namespace VehicleProject.CoreLayer.Services.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);

    }
}
