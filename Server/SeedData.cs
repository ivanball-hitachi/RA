using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using System.Security.Claims;

namespace Server
{
	public class SeedData
	{
		public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>()!.Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context!.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx!.Database.Migrate();
            EnsureUsers(scope);
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var ivan = userMgr.FindByEmailAsync("iballllovera@hitachisolutions.com").Result;
            if (ivan is null)
            {
                ivan = new ApplicationUser
                {
                    UserName = "iballllovera@hitachisolutions.com",
                    Email = "iballllovera@hitachisolutions.com",
                    CustomClaim = "AdminClaim",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(ivan, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        ivan,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "Ivan Ball-llovera"),
                            new Claim(JwtClaimTypes.GivenName, "Ivan"),
                            new Claim(JwtClaimTypes.FamilyName, "Ball-llovera"),
                            new Claim(JwtClaimTypes.WebSite, "http://ivanball.com"),
                            new Claim("location", "somewhere"),
                            //new Claim(JwtClaimTypes.Role, "reviewer"),
                            new Claim(JwtClaimTypes.Role, "admin")
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var ursula = userMgr.FindByEmailAsync("uconley@hitachisolutions.com").Result;
            if (ursula is null)
            {
                ursula = new ApplicationUser
                {
                    UserName = "uconley@hitachisolutions.com",
                    Email = "uconley@hitachisolutions.com",
                    CustomClaim = "ReviewerClaim",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(ursula, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        ursula,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "Ursula Conley"),
                            new Claim(JwtClaimTypes.GivenName, "Ursula"),
                            new Claim(JwtClaimTypes.FamilyName, "Conley"),
                            new Claim(JwtClaimTypes.WebSite, "http://ursulaconley.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "reviewer")
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
	}
}
