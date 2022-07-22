using MAUIBlazorApp.ViewModels.Startup;

namespace MAUIBlazorApp.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}