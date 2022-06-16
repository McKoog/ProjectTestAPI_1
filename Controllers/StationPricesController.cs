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
    public class StationPricesController
    {
        private TableClient client = MyYDBService.Client;
        private StationsPricesYQL yql;
        private readonly ILogger<StationPricesController> _logger;
        public StationPricesController(ILogger<StationPricesController> logger)
        {
            yql = new StationsPricesYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateStationPrice")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public Task<IResponse> CreateStationPrice(
            [Required()]ulong id,
            [Required()]ulong station_id,
            [Required()]ulong fuel_id,
            [Required()]double price)
        {
            return yql.CreateStationPrice(id,station_id,fuel_id,price);
        }

        [HttpGet]
        [Route("GetStationPrices_byFuelId")]
        public string GetStationPrices_byFuelId([Required()]ulong fuel_id){
            return yql.GetStationsPrices_byFuelId(fuel_id);
        }

        [HttpGet]
        [Route("GetStationPrices_byStationId")]
        public string GetStationPrices_byStationId([Required()]ulong station_id){
            return yql.GetStationsPrices_byStationId(station_id);
        }
        [HttpGet]
        [Route("GetStationPrices")]
        public string GetStationPrices(){
            return yql.GetStationsPrices();
        }
        
    }
}