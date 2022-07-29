namespace Domain.Main.DTO
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public char Gender { get; set; }
    }
}
