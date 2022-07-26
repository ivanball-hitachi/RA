﻿using Newtonsoft.Json;
using MAUIBlazorApp.Views.Startup;
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

        private readonly IIdentityService _identityService;
        public LoginPageViewModel(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        #region Commands
        [RelayCommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var response = await _identityService.Authenticate(new LoginRequest
                {
                    UserName = Email,
                    Password = Password
                });

                if(response is not null)
                {
                    //if (response.UserDetail.Role is null)
                    //{
                    //    await AppShell.Current.DisplayAlert("No Role Assigned", "No role assigned to this user.", "OK");
                    //    return;
                    //}

                    if (Preferences.ContainsKey(nameof(_identityService.CurrentUser)))
                    {
                        Preferences.Remove(nameof(_identityService.CurrentUser));
                    }

                    await SecureStorage.SetAsync(nameof(IdentityService.Token), response.Token);

                    string userDetailStr = JsonConvert.SerializeObject(response.UserDetail);
                    Preferences.Set(nameof(_identityService.CurrentUser), userDetailStr);
                    _identityService.Token = response.Token;
                    _identityService.CurrentUser = response.UserDetail;
                    await Shell.Current.GoToAsync($"//{nameof(BlazorPage)}");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Invalid User Name Or Password", "Invalid UserName or Password", "OK");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("User Name And Password Required", "UserName and Password are required", "OK");
            }
        }

        [RelayCommand]
        async void RegisterUser()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
        #endregion
    }
}
