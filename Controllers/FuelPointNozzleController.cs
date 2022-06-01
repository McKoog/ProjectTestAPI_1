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
    public class FuelPointNozzleController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private FuelPointNozzlesYQL yql;
        private readonly ILogger<StationFuelsController> _logger;
        public FuelPointNozzleController(ILogger<StationFuelsController> logger)
        {
            yql = new FuelPointNozzlesYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateFuelPointNozzle")]
        public Task<IResponse> CreateFuelPointNozzle(
            [Required()]ulong id,
            [Required()]ulong fuelPoint_id,
            [Required()]ulong nozzle_id,
            [Required()]ulong fuel_id)
        {
            return yql.CreateFuelPointNozzle(id,fuelPoint_id,nozzle_id,fuel_id);
        }

        [HttpGet]
        [Route("GetNozzles_byFuelPointId")]
        public string GetNozzles_byFuelPointId([Required()]ulong fuelPointId){
            return yql.GetNozzles_byFuelPointId(fuelPointId);
        }
        [HttpGet]
        [Route("GetFuelPointNozzles")]
        public string GetFuelPointNozzles(){
            return yql.GetFuelPointNozzles();
        }
    }
}