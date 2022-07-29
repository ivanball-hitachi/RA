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

        builder.Services.AddBlazoredToast();
        builder.Services.AddAutoMapper(typeof(TimesheetDTOProfile));

        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton(Geolocation.Default);
        builder.Services.AddSingleton(Map.Default);

        // Services Registration
        builder.Services.AddSingleton<IIdentityService, IdentityService>();
        builder.Services.AddSingleton(typeof(IEntityService<,,,>), typeof(EntityService<,,,>));
        builder.Services.AddSingleton<IEntityServices, EntityServices>();

        // Views Registration
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<BlazorPage>();
        builder.Services.AddTransient<DetailsPage>();

        // View Models Registration
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<TimesheetsViewModel>();
        builder.Services.AddTransient<TimesheetDetailsViewModel>();

        return builder.Build();
    }
}
