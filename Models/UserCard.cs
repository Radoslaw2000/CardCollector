namespace CardCollector.Models
{
    public class UserCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Ralations
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid CardId { get; set; }
        public Card? Card { get; set; }

        // Base statistics
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        // Stats
        public int Attack { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int HitPoints { get; set; } = 10;
        public int Speed { get; set; } = 1;
        public string? CustomAvatarUrl { get; set; }

        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
    }

}
