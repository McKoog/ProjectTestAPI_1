using Microsoft.EntityFrameworkCore;
using ProjectTestAPI_1.Models;

namespace ProjectTestAPI_1.SQLiteDb
{
    public class DiscountCardsContext : DbContext
    {
        public DbSet<DiscountCard> discountCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\Users\podrezov\Downloads\ProjectsGIT-WorkHome\SQLiteDB\DiscountCards.db3");
    }
}