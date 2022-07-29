using MAUIBlazorApp.Views.Startup;

namespace MAUIBlazorApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = new AppShellViewModel();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(BlazorPage), typeof(BlazorPage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        // Cancel any back navigation to root (Loading Page).
        if (args.Source == ShellNavigationSource.PopToRoot)
        {
            args.Cancel();
        }
    }
}