using Microsoft.AspNetCore.Mvc;
using Ydb.Sdk.Client;
using ProjectTestAPI_1.SQLiteDb.Repository;
using ProjectTestAPI_1.Models;
using System.Text.Json;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        public StationConfigurationRepository rep;
        private readonly ILogger<DataController> _logger;
        public DataController(ILogger<DataController> logger)
        {
        rep = new StationConfigurationRepository();
        _logger = logger;
        }
        [HttpPost]
        [Route("GetFuelDialogData")]
        public String GetFuelDialogData(ulong stationId)
        {
            fuelDialog data = rep.GetFuelDialogData(stationId);
            return JsonSerializer.Serialize(data);
        }
    }
}