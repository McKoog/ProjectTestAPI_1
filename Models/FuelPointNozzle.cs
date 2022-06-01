namespace ProjectTestAPI_1.Models
{
    public class FuelPointNozzle
    {
        public FuelPointNozzle(ulong id,ulong fuelPointId,ulong nozzleId,ulong fuelId)
        {
            Id = id;
            FuelPointId = fuelPointId;
            NozzleId = nozzleId;
            FuelId = fuelId;
        }
        public ulong Id { get; set; }
        public ulong FuelPointId { get; set; }
        public ulong NozzleId { get; set; }
        public ulong FuelId { get; set; }
    }
}