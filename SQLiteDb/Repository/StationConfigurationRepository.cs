using ProjectTestAPI_1.Models;
using ProjectTestAPI_1.Services;

namespace ProjectTestAPI_1.SQLiteDb.Repository
{
    public class StationConfigurationRepository
    {
        private StationConfigurationContext db;
        public StationConfigurationRepository()
        {
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
            fuelDialog data = new fuelDialog();
            var stationConf = db.stationConfiguration.Where(x => x.StationId == stationId);
            data.FuelPointsAmount = stationConf.Count();
            data.fuelsFullName = new List<string>();

            foreach (var fuelPoint in stationConf)
            {
                data.fuelsFullName.Add(MyYDBService.fuelNames[int.Parse(((fuelPoint.FuelId)-1).ToString())]); 
            }
            return data;

        }
    }
}