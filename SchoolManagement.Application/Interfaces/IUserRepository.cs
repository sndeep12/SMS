using SchoolManagement.Domain.Entities.Auth;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
