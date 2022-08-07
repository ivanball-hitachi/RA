using RazorClassLibrary.Services;

namespace MAUIBlazorApp.ViewModels.Startup
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private char _gender;

        private readonly IIdentityService _identityService;
        public RegisterPageViewModel(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        #region Commands
        [RelayCommand]
        async void RegisterUser()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && 
                !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var success = await _identityService.RegisterUser(new RegisterUserDTO
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    Gender = Gender
                });

                if(success)
                {
                    await AppShell.Current.DisplayAlert("Register User", "User registered successfully", "OK");

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Register User", "Register User failed", "OK");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("Register User", "All fields are required", "OK");
            }
        }
        #endregion
    }
}
