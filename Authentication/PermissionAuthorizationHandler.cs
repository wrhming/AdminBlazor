using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminBlazor.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly UserAccountService _user;

        public PermissionAuthorizationHandler(UserAccountService user)
        {
            _user = user;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (context.User.IsInRole("Admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var userIdClaim = context.User.FindFirst(_ => _.Type == ClaimTypes.Role);
                    if (userIdClaim != null)
                    {
                        var permissions = context.User.FindFirst(_ => _.Type == ClaimTypes.Authentication);
                        //if (_user.CheckPermission(userIdClaim.Value, requirement.Name))
                        //{
                        //    context.Succeed(requirement);
                        //}

                        var rolePermissions = permissions.Value.Split(',');

                        if (rolePermissions.Any(t=> t.StartsWith(requirement.Name)))
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
