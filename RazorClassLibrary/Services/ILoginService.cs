using Domain.Main;
using Domain.Main.DTO;
using RazorClassLibrary.Models;

namespace RazorClassLibrary.Services
{
    public interface ILoginService
    {
        static UserDTO UserDetails = new();
        static string Token = default!;

        Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest);
        Task<List<UserListResponse>> GetAllUsers();
    }
}
