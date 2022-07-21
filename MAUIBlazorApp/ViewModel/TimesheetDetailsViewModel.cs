namespace MAUIBlazorApp.ViewModel;

[QueryProperty(nameof(Timesheet), "Timesheet")]
public partial class TimesheetDetailsViewModel : BaseViewModel
{
    IMap map;
    public TimesheetDetailsViewModel(IMap map)
    {
        this.map = map;
    }

    [ObservableProperty]
    TimesheetDTO timesheet;

    [ICommand]
    async Task OpenMap()
    {
        try
        {
            await Shell.Current.DisplayAlert("Error, no Maps app!", "Not implemented", "OK");
            //await map.OpenAsync(Timesheet.Latitude, Timesheet.Longitude, new MapLaunchOptions
            //{
            //    Name = Timesheet.Name,
            //    NavigationMode = NavigationMode.None
            //});
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }
}
