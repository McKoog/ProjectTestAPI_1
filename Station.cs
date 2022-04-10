namespace ProjectTestAPI_1
{
    public class Station
    {

        public Station(ulong id, bool enable, string? name, string? adress, Location? location)
        {
            Station_id = id;
            Enable = enable;
            Name = name;
            Adress = adress;
            this.location = location;
        }
        public ulong Station_id { get; set; }
        public bool Enable { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public Location? location { get; set; }

    }
}