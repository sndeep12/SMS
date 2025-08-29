using System.Collections.Generic;

namespace SchoolManagement.Domain.Entities.Auth
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
