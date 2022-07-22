namespace Infrastructure.Persistence.IdentityModels
{
    public class UserListResponse
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public char Gender { get; set; }
    }
}
