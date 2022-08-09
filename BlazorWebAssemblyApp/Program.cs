using Blazored.Toast;
using BlazorWebAssemblyApp;
using BlazorWebAssemblyApp.Shared;
using Domain.Common;
using Fluxor;
using Infrastructure.Persistence.Configuration.Timesheets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RazorClassLibrary.Services;
using RazorClassLibrary.Store;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
    builder.Configuration.Bind("UserOptions", options.UserOptions);
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy(
        Policies.CanReviewTimesheets,
        Policies.CanReviewTimesheetsPolicy());
});

builder.Services.AddBlazoredToast();
builder.Services.AddAutoMapper(typeof(TimesheetDTOProfile));

// Services Registration
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped(typeof(IEntityService<,,,>), typeof(EntityService<,,,>));
builder.Services.AddScoped<IEntityServices, EntityServices>();

/*
builder.Services.AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<RolesClaimsPrincipalFactory>();
*/
builder.Services.AddFluxor(options =>
{
    options
        .ScanAssemblies(typeof(CounterFeatureState).Assembly)
        .UseReduxDevTools();
});


await builder.Build().RunAsync();
