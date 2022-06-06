namespace ProjectTestAPI_1.Models
{
    public class StationFPNozzle
    {
        public StationFPNozzle(ulong id,ulong stationId,ulong fuelPointId,ulong nozzleId,ulong fuelId)
        {
            Id = id;
            StationId = stationId;
            FuelPointId = fuelPointId; 
            NozzleId = nozzleId;
            FuelId = fuelId;
        }
        public ulong Id { get; set; }
        public ulong StationId { get; set; }
        public ulong FuelPointId { get; set; }
        public ulong NozzleId { get; set; }
        public ulong FuelId { get; set; }
        
        //public bool highSpeedNozzle { get; set; }
    }
}