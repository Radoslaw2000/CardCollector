namespace CardCollector.Models
{
    public class Trade
    {
        public Guid TradeId { get; set; } = Guid.NewGuid();
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public Guid OfferedCardId { get; set; }
        public Guid RequestedCardId { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
