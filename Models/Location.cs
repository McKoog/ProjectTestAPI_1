namespace ProjectTestAPI_1
{
    public class Location
    {
        public Location(double lat, double Long)
        {
            Lat = lat;
            this.Long = Long;
        }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}