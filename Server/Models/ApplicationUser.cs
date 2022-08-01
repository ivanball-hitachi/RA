using Microsoft.AspNetCore.Identity;

namespace Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? CustomClaim { get; set; } = default!;
    }
}
