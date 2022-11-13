namespace Polimer.Data.Repository.Models
{
    public record UserInfo
    {
        public string? Login { get; init; }
        public string? Role { get; set; }
        public bool Success { get; init; }
    }
}
