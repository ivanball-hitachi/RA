namespace Infrastructure.Persistence.IdentityModels
{
    public class AuthenticateResponse
    {
        public string Token { get; set; } = default!;
        public UserDTO UserDetail { get; set; } = default!;
    }
}
