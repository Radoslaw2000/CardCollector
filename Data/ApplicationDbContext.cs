using CardCollector.Models;
using Microsoft.EntityFrameworkCore;

namespace CardCollector.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<UserCard> UserCards { get; set; }
    public DbSet<Trade> Trades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().HasData(
            new Card { CardId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Card A", Rarity = "Common", Level = 1, Experience = 0 },
            new Card { CardId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Card B", Rarity = "Rare", Level = 1, Experience = 0 }
        );
    }
}

