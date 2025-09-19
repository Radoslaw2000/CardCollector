namespace CardCollector.Models
{
    public class Card
    {
        public Guid CardId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Rarity { get; set; } // "Common","Uncomon","Rare","Epic","Legendary"

        // Base statistics
        public int BaseLevel { get; set; } = 1;
        public int BaseExperience { get; set; } = 0;

        // Combat stats
        public int BaseAttack { get; set; } = 1;
        public int BaseDefense { get; set; } = 1;
        public int BaseHitPoints { get; set; } = 10;
        public int BaseSpeed { get; set; } = 1;

        public string? DefaultImageUrl { get; set; }

        public ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();

    }

}
