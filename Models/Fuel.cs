namespace ProjectTestAPI_1
{
    public class Fuel
    {
        public Fuel(ulong id, string name, string fullName)
        {
            FuelId = id;
            Name = name;
            FullName = fullName;
        }
        public ulong FuelId { get; set; }
        public string Name { get; set; }
        
        public string FullName { get; set; }
        
    }
}