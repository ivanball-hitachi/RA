using Blazored.Toast;
using BlazorWebAssemblyApp.Shared;
using Infrastructure.Persistence.Configuration.Timesheets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RazorClassLibrary.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
    builder.Configuration.Bind("UserOptions", options.UserOptions);
});

builder.Services.AddBlazoredToast();
builder.Services.AddAutoMapper(typeof(TimesheetDTOProfile));

// Services Registration
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped(typeof(IEntityService<,,,>), typeof(EntityService<,,,>));
builder.Services.AddScoped<IEntityServices, EntityServices>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
