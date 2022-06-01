namespace ProjectTestAPI_1.Models
{
    public class StationFuelPoint
    {
        public StationFuelPoint(ulong id,ulong stationId,ulong fuelPointId)
        {
            Id = id;
            StationId = stationId;
            FuelPointId = fuelPointId; 
        }
        public ulong Id { get; set; }
        public ulong StationId { get; set; }
        public ulong FuelPointId { get; set; }

    }
}