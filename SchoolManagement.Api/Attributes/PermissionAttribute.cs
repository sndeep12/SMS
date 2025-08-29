using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolManagement.Application.Interfaces;
using System.Security.Claims;

namespace SchoolManagement.Api
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string module, string submodule, string action)
            : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { module, submodule, action };
        }
    }

    public class PermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly string _module;
        private readonly string _submodule;
        private readonly string _action;

        public PermissionFilter(IPermissionRepository permissionRepository, string module, string submodule, string action)
        {
            _permissionRepository = permissionRepository;
            _module = module;
            _submodule = submodule;
            _action = action;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var perms = await _permissionRepository.GetPermissionsByUserIdAsync(int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"));
            var required = $"{_submodule}:{_action}";

            if (!perms.Contains(required))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
