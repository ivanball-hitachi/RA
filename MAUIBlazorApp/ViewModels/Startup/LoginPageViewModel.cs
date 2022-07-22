using Newtonsoft.Json;
using RazorClassLibrary.Models;
using RazorClassLibrary.Services;

namespace MAUIBlazorApp.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;


        private readonly ILoginService _loginService;
        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        #region Commands
        [ICommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                // calling api 
                var response = await _loginService.Authenticate(new LoginRequest
                {
                    UserName = Email,
                    Password = Password
                });

                if(response is not null)
                {
                    if (response.UserDetail.Role is null)
                    {
                        await AppShell.Current.DisplayAlert("No Role Assigned", 
                            "No role assigned to this user.", "OK");
                        return;
                    }
                    //response.UserDetail.Email = Email;

                    if (Preferences.ContainsKey(nameof(LoginService.UserDetails)))
                    {
                        Preferences.Remove(nameof(LoginService.UserDetails));
                    }

                    await SecureStorage.SetAsync(nameof(LoginService.Token), response.Token);

                    string userDetailStr = JsonConvert.SerializeObject(response.UserDetail);
                    Preferences.Set(nameof(LoginService.UserDetails), userDetailStr);
                    LoginService.UserDetails = response.UserDetail;
                    LoginService.Token = response.Token;
                    await Shell.Current.GoToAsync($"//{nameof(BlazorPage)}");

                }
                else
                {
                    await AppShell.Current.DisplayAlert("Invalid User Name Or Password", "Invalid UserName or Password", "OK");
                }
            }
        }
        #endregion
    }
}
