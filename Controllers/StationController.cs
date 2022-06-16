using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using ProjectTestAPI_1.YQL;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectTestAPI_1.Controllers;

[ApiController]
[Route("[controller]")]
    public class StationController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private StationsYQL yql;
        private readonly ILogger<StationController> _logger;
        public StationController(ILogger<StationController> logger)
        {
            yql = new StationsYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateStation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public Task<IResponse> CreateStation(
            [Required()]ulong id,
            [Required()]bool enable,
            [Required()]string? name,
            [Required()]string? adress,
            [Required()]double lat,
            [Required()]double Long)
        {
            return yql.CreateStation(id,enable,name,adress,lat,Long);
        }
            
        [HttpGet]
        [Route("GetStation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetStation([Required()]ulong id){
            return yql.GetStation(id);
        }

        [HttpGet]
        [Route("GetStations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetStations(){
            return yql.GetStations();
        }
        
    }

