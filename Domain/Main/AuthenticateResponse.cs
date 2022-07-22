using Domain.Main.DTO;

namespace Domain.Main
{
    public class AuthenticateResponse
    {
        public string Token { get; set; } = default!;
        public UserDTO UserDetail { get; set; } = default!;
    }
}
