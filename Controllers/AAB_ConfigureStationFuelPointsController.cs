using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Models;
using System.Text.Json;
using ProjectTestAPI_1.SQLiteDb.Repository;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AAB_ConfigureStationFuelPointsController : ControllerBase
    {
        private readonly ILogger<AAB_ConfigureStationFuelPointsController> _logger;
        public StationConfigurationRepository rep;
        public AAB_ConfigureStationFuelPointsController(ILogger<AAB_ConfigureStationFuelPointsController> logger)
        {
        rep = new StationConfigurationRepository();
        _logger = logger;
        }

        [HttpPost(Name = "Configure")]
        public void Configure(JsonDocument Configuration)
        {
            var config = JsonSerializer.Deserialize<StationConfiguration>(Configuration);
            rep.SetConfiguration(config!);
        }
    }
}