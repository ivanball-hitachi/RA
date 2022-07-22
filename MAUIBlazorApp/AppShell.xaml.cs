namespace MAUIBlazorApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = new AppShellViewModel();

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}