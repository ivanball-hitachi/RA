using MAUIBlazorApp.Helpers;
using MAUIBlazorApp.Views.Startup;
using RazorClassLibrary.Services;

namespace MAUIBlazorApp.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {

        [ICommand]
        async void SignOut()
        {
            var _loginServive = ServiceHelper.GetService<IdentityService>();
            if (Preferences.ContainsKey(nameof(_loginServive.CurrentUser)))
            {
                Preferences.Remove(nameof(_loginServive.CurrentUser));
            }
            _loginServive.SignOut();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
