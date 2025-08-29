using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;


namespace SchoolManagement.Api.Controllers
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context) { }

        public async Task<List<string>> GetPermissionsByUserIdAsync(int userId)
        {
            return await (from ur in _context.UserRoles
                          join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                          join p in _context.Permissions.Include(p => p.SubModule) on rp.PermissionId equals p.PermissionId
                          where ur.UserId == userId
                          select $"{p.SubModule.Name}:{p.Action}").ToListAsync();
        }
    }
}
