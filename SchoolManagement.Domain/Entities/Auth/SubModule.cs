namespace SchoolManagement.Domain.Entities.Auth
{
    public class SubModule
    {
        public int SubModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public Module? Module { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
