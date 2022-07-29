using Domain.Main;
using Domain.Main.DTO;
using RazorClassLibrary.Models;

namespace RazorClassLibrary.Services
{
    public interface IIdentityService
    {
        string Token { get; set; }
        UserDTO CurrentUser { get; set; }
        string CurrentUserName { get; }

        Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterUserDTO userDetails);
        void SignOut();
        Task<string> GetToken();
        Task<List<UserListResponse>> GetAllUsers();
    }
}
