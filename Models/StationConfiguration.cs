namespace ProjectTestAPI_1.Models
{
    public class StationConfiguration
    {
        public StationConfiguration(List<StationFPNozzle> configuration)
        {
            Configuration = configuration;
        }
        public List<StationFPNozzle> Configuration { get; set; }
    }
}