namespace ProjectTestAPI_1
{
    public class StationPrices_JsonByFuelId
    {
        public StationPrices_JsonByFuelId(ulong id, ulong fuelId, ulong stationId, double price)
        {
            Id = id;
            FuelId = fuelId;
            StationId = stationId;
            Price = price;
        }
        public ulong Id { get; set; }
        public ulong FuelId { get; set; }
        public ulong StationId { get; set; }
        public double Price { get; set; }       
        
    }
    public class StationPrices_JsonByStationId
    {
        public StationPrices_JsonByStationId(ulong id, ulong stationId,ulong fuelId, double price)
        {
            Id = id;
            StationId = stationId;
            FuelId = fuelId;
            Price = price;
        }
        public ulong Id { get; set; }
        public ulong StationId { get; set; }
        public ulong FuelId { get; set; }
        public double Price { get; set; }       
        
    }
}