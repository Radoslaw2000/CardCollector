namespace CardCollector.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid Token { get; set; } = Guid.NewGuid();
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; } = false;

        // Relations to user
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
