namespace ProjectTestAPI_1.Models
{
    public class fuelDialog
    {
        public List<fuelPointData> FPData { get; set; }
        //public fuelPriceData PriceData { get; set; }
        
    }
    public class fuelPointData
    {
        public ulong id { get; set; }
        public List<fuelsInfo> fuelsInfo { get; set; }

    }
    public class fuelsInfo
    {
        public ulong fuelId { get; set; }
        public String fuelName { get; set; }
        public ulong priceId { get; set; }
        public double fuelPrice { get; set; }
    }/*
    public class fuelPriceData
    {
        public List<priceInfo> priceInfo { get; set; }
    }
    public class priceInfo
    {
        public ulong fuelId { get; set; }
        public double fuelPrice { get; set; }
    }*/
}