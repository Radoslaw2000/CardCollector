namespace CardCollector.Models
{
    public class UserCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
    }

}
