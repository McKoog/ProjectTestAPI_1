using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using ProjectTestAPI_1.YQL;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationFuelsController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private StationsFuelsYQL yql;
        private readonly ILogger<StationFuelsController> _logger;
        public StationFuelsController(ILogger<StationFuelsController> logger)
        {
            yql = new StationsFuelsYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateStationFuel")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public Task<IResponse> CreateStationFuel(
            [Required()]ulong id,
            [Required()]ulong station_id,
            [Required()]ulong fuel_id,
            [Required()]String fuel_name)
        {
            return yql.CreateStationFuel(id,station_id,fuel_id,fuel_name);
        }

        [HttpGet]
        [Route("GetStationFuels_byFuelId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetStationFuels_byFuelId([Required()]ulong fuel_id){
            return yql.GetStationFuels_byFuelId(fuel_id);
        }

        [HttpGet]
        [Route("GetStationFuels_byStationId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetStationFuels_byStationId([Required()]ulong station_id){
            return yql.GetStationFuels_byStationId(station_id);
        }
        [HttpGet]
        [Route("GetStationFuels")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetStationFuels(){
            return yql.GetStationFuels();
        }
    }
}