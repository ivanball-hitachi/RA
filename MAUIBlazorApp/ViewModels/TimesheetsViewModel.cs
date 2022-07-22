using RazorClassLibrary.Services;

namespace MAUIBlazorApp.ViewModels;

public partial class TimesheetsViewModel : BaseViewModel
{
    public ObservableCollection<TimesheetDTO> Timesheets { get; } = new();
    IEntityServices entityServices;
    IConnectivity connectivity;
    IGeolocation geolocation;
    public TimesheetsViewModel(IEntityServices entityServices, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Timesheets";
        this.entityServices = entityServices;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }
    
    [ICommand]
    async Task GoToDetails(TimesheetDTO timesheet)
    {
        if (timesheet is null)
        return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Timesheet", timesheet }
        });
    }

    [ObservableProperty]
    bool isRefreshing;

    [ICommand]
    async Task GetTimesheetsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var (timesheets, _) = await entityServices.TimesheetService.GetAllAsync("");

            if(Timesheets.Count != 0)
                Timesheets.Clear();

            foreach(var timesheet in timesheets)
                Timesheets.Add(timesheet);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get timesheets: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }

    [ICommand]
    async Task GetClosestTimesheet()
    {
        if (IsBusy || Timesheets.Count == 0)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location is null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            // Find closest timesheet to us
            //var first = Timesheets.OrderBy(m => location.CalculateDistance(
            //    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
            //    .FirstOrDefault();

            //await Shell.Current.DisplayAlert("", first.Name + " " +
            //    first.Location, "OK");

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
