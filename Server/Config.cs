/*
Startup Project: Server
Package Manager Console Default Project: Server
Add-Migration -Context PersistedGrantDBContext InitialIdentityServerMigration 
Add-Migration -Context ConfigurationDBContext InitialIdentityServerMigration
Update-Database -Context PersistedGrantDBContext
Update-Database -Context ConfigurationDBContext
Add-Migration -Context AspNetIdentityDBContext InitialAspNetIdentityMigration
Update-Database -Context AspNetIdentityDBContext
dotnet run .\Server\bin\Debug\net6.0\Server /seed --project Server

Startup Project: WebAPI
Package Manager Console Default Project: Infrastructure
Add-Migration -Context ApplicationDBContext InitialMigration
Add-Migration -Context LoginFlowDBContext InitialMigration
Update-Database -Context ApplicationDBContext
Update-Database -Context LoginFlowDBContext
*/
using IdentityServer4.Models;
using System.Security.Claims;

namespace Server
{
	public class Config
	{
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                //new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("role", new List<string> { "role" }),
                new IdentityResource("custom_claim", new List<string> { "custom_claim" }),
                new IdentityResource("openid", new List<string> { "openid", "role", "custom_claim" }),
                new IdentityResource("country", new List<string> { "country" }),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("hisoltimesheetapi") };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("hisoltimesheetapi", "Hitachi Solutions Timesheet API")
                {
                    Scopes = new List<string> { "hisoltimesheetapi" },
                    //ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "country", "role" }
                },
                new ApiResource("role",
                    new [] { "role" }),
                new ApiResource("custom_claim",
                    new [] { "custom_claim" })
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
                    AllowedScopes = { "openid", "profile", "email", "hisoltimesheetapi", "country", "role", "custom_claim" },
                    RequireConsent = false
                },
            };
    }
}
