using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Data;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(defaultConnString);
}

builder.Services.AddMvc();

builder.Services.AddTransient<IEmailSender, DummyEmailSender>();

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlServer(defaultConnString,
        b => b.MigrationsAssembly(assembly)));

//// configure IIS out-of-proc settings
//builder.Services.Configure<IISOptions>(iis =>
//{
//    iis.AuthenticationDisplayName = "Windows";
//    iis.AutomaticAuthentication = false;
//});
//// configure IIS in-proc settings
//builder.Services.Configure<IISServerOptions>(iis =>
//{
//    iis.AuthenticationDisplayName = "Windows";
//    iis.AutomaticAuthentication = false;
//});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AspNetIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();;
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.Run();
