namespace TenexCars.DTOs
{
    public class EmailDto
    {
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? UserName { get; set; }
        public string? Otp { get; set; }
        public string? ResetLink { get; set; }
    }
}
