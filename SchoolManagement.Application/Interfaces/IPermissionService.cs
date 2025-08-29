namespace SchoolManagement.Application.Interfaces
{
    public interface IPermissionService
    {
        bool HasPermission(int userId, string module, string permission);
    }
}
