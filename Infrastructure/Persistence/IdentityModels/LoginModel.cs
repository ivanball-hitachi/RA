namespace Infrastructure.Persistence.IdentityModels
{
    public class LoginModel
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
