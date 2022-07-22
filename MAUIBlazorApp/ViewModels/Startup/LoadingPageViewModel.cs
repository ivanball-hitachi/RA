using MAUIBlazorApp.Views.Startup;
using Newtonsoft.Json;
using RazorClassLibrary.Models;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBlazorApp.ViewModels.Startup
{
    public class LoadingPageViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(LoginService.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                // navigate to Login Page
            }
            else
            {
               var tokenDetails = await SecureStorage.GetAsync(nameof(LoginService.Token));

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenDetails) as JwtSecurityToken;

                if(jsonToken.ValidTo < DateTime.UtcNow)
                {
                    await Shell.Current.DisplayAlert("User session expired", "Login Again To conitnue", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    var userInfo = JsonConvert.DeserializeObject<UserDTO>(userDetailsStr);
                    LoginService.UserDetails = userInfo;
                    LoginService.Token = tokenDetails;
                    //await AppConstant.AddFlyoutMenusDetails();
                }
            }
        }

    }
}
