using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;
using ProjectTestAPI_1.YQL;
using System.ComponentModel.DataAnnotations;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationFuelPointController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private StationsFuelPointsYQL yql;
        private readonly ILogger<StationFuelsController> _logger;
        public StationFuelPointController(ILogger<StationFuelsController> logger)
        {
            yql = new StationsFuelPointsYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateStationsFuelPoint")]
        public Task<IResponse> CreateStationFuelPoint(
            [Required()]ulong id,
            [Required()]ulong station_id,
            [Required()]ulong fuelPoint_id)
        {
            return yql.CreateStationFuelPoint(id,station_id,fuelPoint_id);
        }

        [HttpGet]
        [Route("GetStationsFuelPoints_byStationId")]
        public string GetStationFuels_byStationId([Required()]ulong station_id){
            return yql.GetStations_byStationId(station_id);
        }
        [HttpGet]
        [Route("GetStationsFuelPoints")]
        public string GetStationFuels(){
            return yql.GetStationsFuelsPoints();
        }
    }
}