using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OpsHubDAL.UserRolePerRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Helper.AuthHandlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _scopeFactory;

        public PermissionHandler( IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            List<string> permissions = new List<string>();
            var userId = context.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId)) return;
            var userType = context.User.FindFirst("Type")?.Value;
            if (!string.IsNullOrEmpty(userType))
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IUserRolePerRepository>();
                permissions = userType switch
                {
                    "TenantUser" => await repo.GetUserPermissionsByUserId(int.Parse(userId)),
                    "Tenant" => new List<string> { "create_admin", "subscribe_plan" },
                    "SystemAdmin" => new List<string> { "create_tenant" },
                    _ => new List<string>()
                };
            }
            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
