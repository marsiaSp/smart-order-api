namespace SmartOrder.API.DTOs
{
    public class RegisterDTO
    {
        public string StoreName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}