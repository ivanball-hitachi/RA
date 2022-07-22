namespace Domain.Main.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Role { get; set; }
        public int? RoleID { get; set; }
    }
}
