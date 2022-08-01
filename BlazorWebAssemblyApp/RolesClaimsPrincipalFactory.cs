using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorWebAssemblyApp
{
    public class RolesClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public RolesClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);
            if (user.Identity!.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)user.Identity;
                var roleClaims = identity.FindAll("role").ToArray();
                if (roleClaims != null && roleClaims.Any())
                {
                    foreach (var existingClaim in roleClaims)
                    {
                        var roleClaimValues = existingClaim.Value.Replace("[", "").Replace("]", "").Replace("\"", "").Split(",");
                        foreach (var roleClaimValue in roleClaimValues)
                        {
                            identity.AddClaim(new Claim("role", roleClaimValue.ToString()));
                        }
                        identity.RemoveClaim(existingClaim);
                    }

                    //var rolesElem = account.AdditionalProperties["role"];
                    //if (rolesElem is JsonElement roles)
                    //{
                    //    if (roles.ValueKind == JsonValueKind.Array)
                    //    {
                    //        foreach (var role in roles.EnumerateArray())
                    //        {
                    //            identity.AddClaim(new Claim(options.RoleClaim, role.GetString()!));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        identity.AddClaim(new Claim(options.RoleClaim, roles.GetString()!));
                    //    }
                    //}
                }
            }
            return user;
        }
    }
}
