using ProjectTestAPI_1.Models;
using ProjectTestAPI_1.Services;
using ProjectTestAPI_1.YQL;
using Ydb.Sdk.Table;

namespace ProjectTestAPI_1.SQLiteDb.Repository
{
    public class StationConfigurationRepository
    {
        private StationsPricesYQL yqlPrice;
        private StationsFuelsYQL yqlFuelNames;
        private TableClient client = MyYDBService.Client;
        private StationConfigurationContext db;
        public StationConfigurationRepository()
        {
            yqlPrice = new StationsPricesYQL(client);
            yqlFuelNames = new StationsFuelsYQL(client);
            db = new StationConfigurationContext();
        }
        public void SetConfiguration(StationConfiguration config){
            
            db.stationConfiguration.RemoveRange(db.stationConfiguration.Where(x => x.StationId == config.Configuration[0].StationId));
            db.SaveChanges();

            foreach (var SFPNozzle in config.Configuration)
            {
                db.stationConfiguration.Add(SFPNozzle);
            }
            db.SaveChanges();
        }
        
        public fuelDialog GetFuelDialogData(ulong stationId){

            fuelDialog FuelDialog = new fuelDialog();
            FuelDialog.FPData = new List<fuelPointData>();

            var stationConf = db.stationConfiguration.Where(x => x.StationId == stationId).ToList().OrderBy(x => x.FuelPointId).ToList();

            List<StationFuel_JsonByStation> stationFuels = new List<StationFuel_JsonByStation>();
            List<StationPrice_JsonByStationId> stationPrice = new List<StationPrice_JsonByStationId>();
            
            stationFuels = yqlFuelNames.GetStationFuels_byStationId_ListObject_NotJson(stationId);
            stationPrice = yqlPrice.GetStationsPrices_ListOfObjects_NotJson(stationId);

            while(stationConf.Count() > 0){

            fuelPointData FPdata = new fuelPointData();
            FPdata.fuelsInfo = new List<fuelsInfo>();
            
            var fuelPoint = stationConf.Where(x => x.FuelPointId == stationConf.Min(z => z.FuelPointId)).ToList();
            FPdata.id = fuelPoint.First().FuelPointId;

            foreach (var fp in fuelPoint)
            {
                fuelsInfo info = new fuelsInfo();
                info.fuelPrice = new double();
                var x = stationFuels.Find(x => x.FuelId == fp.FuelId);
                if(x != null){
                    info.fuelName = x.FuelName;
                    info.fuelId = x.FuelId;
                    info.fuelPrice = stationPrice.Find(z => z.FuelId == x.FuelId)!.Price;
                    info.priceId = stationPrice.Find(z => z.FuelId == x.FuelId)!.Id;
                    FPdata.fuelsInfo.Add(info);
                }
                else{
                    info.fuelName = MyYDBService.fuelNames[int.Parse((fp.FuelId).ToString())];
                    info.fuelId = fp.FuelId;
                    info.fuelPrice = stationPrice.Find(z => z.FuelId == fp.FuelId)!.Price;
                    info.priceId = stationPrice.Find(z => z.FuelId == fp.FuelId)!.Id;
                    FPdata.fuelsInfo.Add(info);
                }
            }
            stationConf.RemoveRange(0,fuelPoint.Count());
            FuelDialog.FPData.Add(FPdata);
            }
            return FuelDialog;
        }
    }
}