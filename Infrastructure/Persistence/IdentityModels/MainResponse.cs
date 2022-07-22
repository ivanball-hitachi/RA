namespace Infrastructure.Persistence.IdentityModels
{
    public class MainResponse
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; } = default!;

        public string ErrorMessage { get; set; } = default!;
    }
}
