using MAUIBlazorApp.ViewModels.Startup;

namespace MAUIBlazorApp.Views.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}