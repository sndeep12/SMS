

using SchoolManagement.Domain.Entities.Auth;

namespace SchoolManagement.Application.Interfaces
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<List<string>> GetPermissionsByUserIdAsync(int userId);
    }
}
