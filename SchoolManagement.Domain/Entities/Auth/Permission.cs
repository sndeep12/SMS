namespace SchoolManagement.Domain.Entities.Auth
{
    public class Permission
    {
        public int PermissionId{ get; set; }
        public string Action { get; set; } = string.Empty; // read, write, delete, export
        public int SubModuleId { get; set; }
        public SubModule? SubModule { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
