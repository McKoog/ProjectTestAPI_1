using Microsoft.EntityFrameworkCore;
using ProjectTestAPI_1.Models;

namespace ProjectTestAPI_1
{
    public class StationConfigurationContext : DbContext
    {
        public DbSet<StationFPNozzle> stationConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\Users\podrezov\Downloads\ProjectsGIT-WorkHome\SQLiteDB\StationConfiguration.db3");
    }
}