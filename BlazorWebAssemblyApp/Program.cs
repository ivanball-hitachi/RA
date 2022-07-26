using Blazored.Toast;
using Infrastructure.Persistence.Configuration.Timesheets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RazorClassLibrary;
using RazorClassLibrary.Services;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredToast();
builder.Services.AddAutoMapper(typeof(TimesheetDTOProfile));

// Services Registration
builder.Services.AddSingleton<IIdentityService, IdentityService>();
builder.Services.AddSingleton(typeof(IEntityService<,,,>), typeof(EntityService<,,,>));
builder.Services.AddSingleton<IEntityServices, EntityServices>();
builder.Services.AddSingleton<ITimesheetLineService, TimesheetLineService>();

await builder.Build().RunAsync();
