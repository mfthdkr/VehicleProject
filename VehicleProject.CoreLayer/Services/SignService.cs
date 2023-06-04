using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VehicleProject.CoreLayer.Services
{
    public static class SignService
    {   
        // Simetrik imzalama, şifreleme
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
        
    }
}
