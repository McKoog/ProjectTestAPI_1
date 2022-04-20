namespace ProjectTestAPI_1
{
    public class StationFuels_JsonByStation
    {
        public StationFuels_JsonByStation(ulong id,ulong stationId,ulong fuelId)
        {
            Id = id;
            StationId = stationId;
            FuelId = fuelId;
        }
        public ulong Id {get; set;}
        public ulong StationId { get; set; }
        
        public ulong FuelId { get; set; } 
        
    }

    public class StationFuels_JsonByFuel 
    {
        public StationFuels_JsonByFuel(ulong id,ulong fuelId,ulong stationId)
        {
            Id = id;
            FuelId = fuelId;
            StationId = stationId;            
        }
        public ulong Id {get; set;}
        public ulong FuelId { get; set; } 
        public ulong StationId { get; set; }


    }
}