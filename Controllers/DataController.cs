using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.SQLiteDb.Repository;
using ProjectTestAPI_1.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
    public class DataController : ControllerBase
    {
        public StationConfigurationRepository rep;
        private readonly ILogger<DataController> _logger;
        public DataController(ILogger<DataController> logger)
        {
        rep = new StationConfigurationRepository();
        _logger = logger;
        }
        [HttpGet]
        [Route("GetFuelDialogData")]
        public String GetFuelDialogData(ulong stationId)
        {
            fuelDialog data = rep.GetFuelDialogData(stationId);
            return JsonSerializer.Serialize(data);
        }
    }
}