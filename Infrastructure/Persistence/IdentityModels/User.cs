using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.IdentityModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public char Gender { get; set; }
    }
}
