namespace MAUIBlazorApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(TimesheetDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}