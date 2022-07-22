namespace Infrastructure.Persistence.IdentityModels
{
    public class AssignRoleRequest
    {
        public string Email { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }
}
