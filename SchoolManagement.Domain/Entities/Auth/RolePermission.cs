namespace SchoolManagement.Domain.Entities.Auth
{
    public class RolePermission
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
