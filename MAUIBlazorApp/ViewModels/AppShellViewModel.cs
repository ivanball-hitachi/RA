using MAUIBlazorApp.Views.Startup;
using RazorClassLibrary.Services;

namespace MAUIBlazorApp.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {

        [ICommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(LoginService.UserDetails)))
            {
                Preferences.Remove(nameof(LoginService.UserDetails));
            }
            LoginService.UserDetails = null;
            LoginService.Token = null;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
