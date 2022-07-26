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
            var _identityService = ServiceHelper.GetService<IIdentityService>();
            if (Preferences.ContainsKey(nameof(_identityService.CurrentUser)))
            {
                Preferences.Remove(nameof(_identityService.CurrentUser));
            }
            _identityService.SignOut();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
