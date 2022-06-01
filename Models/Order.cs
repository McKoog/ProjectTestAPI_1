namespace ProjectTestAPI_1.Models
{
    public class Order
    {
        public Order(
            ulong id, 
            DateTime dateCreate,
             string orderType,
             double orderVolume,
             ulong stationId,
             ulong columnId,
             ulong fuelId,
             string fuelMarka,
             ulong priceId,
             double priceFuel,
             double sum,
             double litres,
             string contractId)
        {
            DateCreate = dateCreate;
            OrderType = orderType;
            orderVolume = OrderVolume;
            StationId = stationId;
            ColumnId = columnId;
            FuelId = fuelId;
            FuelMarka = fuelMarka;
            PriceId = priceId;
            PriceFuel = priceFuel;
            Sum = sum;
            Litres = litres;
            ContractId = contractId;
        }
        public ulong Id { get; set; }
        
        public DateTime DateCreate { get; set; }
        
        public string OrderType { get; set; }
        
        public double OrderVolume { get; set; }
        
        public ulong StationId { get; set; }
        
        public ulong StationExtendedId { get; set; }
        
        public ulong ColumnId { get; set; }
        
        public ulong FuelId { get; set; }
        
        public string FuelMarka { get; set; }
        
        public ulong PriceId { get; set; }
        
        public ulong FuelExtendedId { get; set; }
        
        public double PriceFuel { get; set; }
        
        public double Sum { get; set; }
        
        public double Litres { get; set; }
        
        public double SumPaid { get; set; }
        
        public string OrderStaus { get; set; }
        
        public string Message { get; set; }
        
        public DateTime DateEnd { get; set; }
        
        public ulong ReasonId { get; set; }
        
        public string Reason { get; set; }
        
        public double LitreCompleted { get; set; }
        
        public double SumPaidCompleted { get; set; }
        
        public string ContractId { get; set; }
        
        public double CommisionPercent { get; set; }
        
        public double SumWithoutComission { get; set; }
        
        public int mAppVote { get; set; }
        
        public string UserNote { get; set; }
        
        public double Discount { get; set; }
        
        public double DiscountedPrice { get; set; }
        
        public ulong DiscountSystemId { get; set; }
        
        public ulong DiscountCardId { get; set; }
        
        public int BonusesAdded { get; set; }
        
        public int BonusesSpent { get; set; }
        
        public double BonusPrice { get; set; }
        
        
    }
}