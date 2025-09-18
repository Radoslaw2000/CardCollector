namespace CardCollector.Models
{
    public class Card
    {
        public Guid CardId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Rarity { get; set; } // "Common","Uncomon","Rare","Epic","Legendary"
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

    }

}
