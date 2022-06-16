namespace ProjectTestAPI_1
{
    public class StationFuel_JsonByStation
    {
        public StationFuel_JsonByStation(ulong id,ulong stationId,ulong fuelId,String fuelName)
        {
            Id = id;
            StationId = stationId;
            FuelId = fuelId;
            FuelName = fuelName;
        }
        public ulong Id {get; set;}
        public ulong StationId { get; set; }
        
        public ulong FuelId { get; set; } 
        public String FuelName { get; set; }
        
    }

    public class StationFuel_JsonByFuel 
    {
        public StationFuel_JsonByFuel(ulong id,ulong fuelId,ulong stationId,String fuelName)
        {
            Id = id;
            FuelId = fuelId;
            StationId = stationId;
            FuelName = fuelName;         
        }
        public ulong Id {get; set;}
        public ulong FuelId { get; set; } 
        public ulong StationId { get; set; }
        public String FuelName { get; set; }
    }
}