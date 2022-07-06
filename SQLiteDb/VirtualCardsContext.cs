using Microsoft.EntityFrameworkCore;
using ProjectTestAPI_1.Models;

namespace ProjectTestAPI_1.SQLiteDb
{
    public class VirtualCardsContext : DbContext
    {
        public DbSet<VirtualCard> virtualCards { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\Users\podrezov\Downloads\ProjectsGIT-WorkHome\SQLiteDB\VirtualCards.db3");
    }
}