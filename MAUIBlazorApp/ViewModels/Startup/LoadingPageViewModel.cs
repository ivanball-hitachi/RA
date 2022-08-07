using MAUIBlazorApp.Views.Startup;
using Newtonsoft.Json;
using RazorClassLibrary.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MAUIBlazorApp.ViewModels.Startup
{
    public class LoadingPageViewModel
    {
        private readonly IIdentityService _identityService;
        public LoadingPageViewModel(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(_identityService.CurrentUser), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                // navigate to Login Page
            }
            else
            {
               var tokenDetails = await SecureStorage.GetAsync(nameof(_identityService.Token));

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenDetails) as JwtSecurityToken;

                if(jsonToken.ValidTo < DateTime.UtcNow)
                {
                    //await Shell.Current.DisplayAlert("User session expired", "Login again To continue", "OK");
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                }
                else
                {
                    var userInfo = JsonConvert.DeserializeObject<UserDTO>(userDetailsStr);
                    _identityService.Token = tokenDetails;
                    _identityService.CurrentUser = userInfo;
                    await Shell.Current.GoToAsync(nameof(BlazorPage));
                }
            }
        }

    }
}
