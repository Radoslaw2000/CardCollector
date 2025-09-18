namespace CardCollector.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public Guid? AccessToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
