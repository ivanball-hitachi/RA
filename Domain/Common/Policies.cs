using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Domain.Common
{
    public static class Policies
    {
        public const string CanReviewTimesheets = "CanReviewTimesheets";

        public static AuthorizationPolicy CanReviewTimesheetsPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRoleExtension("reviewer")
                .Build();
        }
    }

    public static class RoleAuthorizationRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireRoleExtension(
            this AuthorizationPolicyBuilder authorizationPolicyBuilder,
            params string[] requiredRoles)
        {
            authorizationPolicyBuilder.RequireRoleExtension((IEnumerable<string>)requiredRoles);
            return authorizationPolicyBuilder;
        }

        public static AuthorizationPolicyBuilder RequireRoleExtension(
            this AuthorizationPolicyBuilder authorizationPolicyBuilder,
            IEnumerable<string> requiredRoles)
        {
            authorizationPolicyBuilder.AddRequirements(new RoleAuthorizationRequirement(requiredRoles));
            return authorizationPolicyBuilder;
        }
    }

    public class RoleAuthorizationRequirement : AuthorizationHandler<RoleAuthorizationRequirement>, IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredRoles { get; }

        public RoleAuthorizationRequirement(IEnumerable<string> requiredRoles)
        {
            if (requiredRoles == null || !requiredRoles.Any())
            {
                throw new ArgumentException($"{nameof(requiredRoles)} must contain at least one value.", nameof(requiredRoles));
            }

            RequiredRoles = requiredRoles;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                // Presentation UI Claim check
                var roleClaim = context.User.Claims.FirstOrDefault(
                    c => string.Equals(c.Type, "role", StringComparison.OrdinalIgnoreCase));
                if (roleClaim is not null)
                {
                    var roles = roleClaim.Value;
                    if (requirement.RequiredRoles.All(requiredRole => roles.Contains(requiredRole)))
                    {
                        context.Succeed(requirement);
                    }
                }

                // Presentation WebAPI Claim check
                var identity = (ClaimsIdentity)context.User.Identity!;
                var roleClaims = identity.FindAll(ClaimTypes.Role).ToArray();
                if (roleClaims is not null)
                {
                    foreach (var roleClaim2 in roleClaims)
                    {
                        var roles = roleClaim2.Value;
                        if (requirement.RequiredRoles.All(requiredRole => roles.Contains(requiredRole)))
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
