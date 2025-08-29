

using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<List<string>> GetPermissionsByUserIdAsync(int userId)
        {
            return await _permissionRepository.GetPermissionsByUserIdAsync(userId);
        }

        public bool HasPermission(int userId, string module, string permission)
        {
            return true; // Placeholder implementation
            throw new NotImplementedException();
        }
    }
}
