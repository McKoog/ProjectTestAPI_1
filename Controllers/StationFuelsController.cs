using System.Xml;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public class StationFuelsController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private StationFuelsYQL yql;
        private readonly ILogger<StationFuelsController> _logger;
        public StationFuelsController(ILogger<StationFuelsController> logger)
        {
            yql = new StationFuelsYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateStationFuel")]
        public Task<IResponse> CreateStation(
            [Required()]ulong id,
            [Required()]ulong station_id,
            [Required()]ulong fuel_id)
        {
            return yql.CreateStationFuel(id,station_id,fuel_id);
        }

        [HttpGet]
        [Route("GetStationFuels_byFuelId")]
        public string GetStationFuels_byFuelId([Required()]ulong fuel_id){
            return yql.GetStations_byFuelId(fuel_id);
        }

        [HttpGet]
        [Route("GetStationFuels_byStationId")]
        public string GetStationFuels_byStationId([Required()]ulong station_id){
            return yql.GetStations_byStationId(station_id);
        }
        [HttpGet]
        [Route("GetStationFuels")]
        public string GetStationFuels(){
            return yql.GetStationsFuels();
        }
    }
}