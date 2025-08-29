using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Domain.Entities.Auth;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAuthService
    {
       // LoginResponse GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken(string ipAddress);

       

    }
}
