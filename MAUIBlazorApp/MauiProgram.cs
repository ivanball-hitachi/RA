using RazorClassLibrary.Services;
using Blazored.Toast;
using Infrastructure.Persistence.Configuration.Timesheets;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using MAUIBlazorApp.Views.Startup;
using MAUIBlazorApp.ViewModels.Startup;

namespace MAUIBlazorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazoredToast();

        var appSettingsFileName = typeof(MauiProgram).Namespace;

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        appSettingsFileName += ".appsettings.Development.json";
#else
        appSettingsFileName += ".appsettings.json";
#endif

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(appSettingsFileName);
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
        builder.Configuration.AddConfiguration(config);

        builder.Services.AddAutoMapper(typeof(TimesheetDTOProfile));

        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton(Geolocation.Default);
        builder.Services.AddSingleton(Map.Default);

        // Services
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton(typeof(IEntityService<,,,>), typeof(EntityService<,,,>));
        builder.Services.AddSingleton<IEntityServices, EntityServices>();
        builder.Services.AddSingleton<ITimesheetLineService, TimesheetLineService>();

        // Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<BlazorPage>();
        builder.Services.AddTransient<DetailsPage>();

        // View Models
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<TimesheetsViewModel>();
        builder.Services.AddTransient<TimesheetDetailsViewModel>();

        return builder.Build();
    }
}
