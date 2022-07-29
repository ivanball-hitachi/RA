using MAUIBlazorApp.ViewModels.Startup;

namespace MAUIBlazorApp.Views.Startup;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}