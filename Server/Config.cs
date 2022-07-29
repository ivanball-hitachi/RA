/*
Add-Migration InitialIdentityServerMigration -c PersistedGrantDBContext
Add-Migration InitialIdentityServerMigration -c ConfigurationDBContext
Update-Database -Context PersistedGrantDBContext
Update-Database -Context ConfigurationDBContext
Add-Migration InitialAspNetIdentityMigration -c AspNetIdentityDBContext
Update-Database -Context AspNetIdentityDBContext
dotnet run .\Server\bin\Debug\net6.0\Server /seed --project Server
*/
using IdentityServer4.Models;

namespace Server
{
	public class Config
	{
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("country", new List<string> { "country" }),
                new IdentityResource("role", new List<string> { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("hisoltimesheetapi") };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("hisoltimesheetapi", "Hitachi Solutions Timesheet API",
                    new [] { "country" })
                //new ApiResource("hisoltimesheetapi", "Hitachi Solutions Timesheet API")
                //{
                //    Scopes = new List<string> { "hisoltimesheetapi.read", "hisoltimesheetapi.write" },
                //    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                //    UserClaims = new List<string> { "country", "role" }
                //}
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "hisoltimesheet",
                    ClientName = "Hitachi Solutions Timesheet",
                    //ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = { "https://localhost:7090/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:7090/authentication/logout-callback" },
                    AllowedCorsOrigins = { "https://localhost:7090" },
                    AllowedScopes = { "openid", "profile", "email", "hisoltimesheetapi", "country" },
                    RequireConsent = false
                },
            };
    }
}
