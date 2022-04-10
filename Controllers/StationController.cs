using System.Xml;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;
using ProjectTestAPI_1.YqlScript;

namespace ProjectTestAPI_1.Controllers;

[ApiController]
[Route("[controller]")]
    public class StationController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private YQL yql;
        private readonly ILogger<StationController> _logger;
        public StationController(ILogger<StationController> logger)
        {
            yql = new YQL(client);
            _logger = logger;
        }
        [HttpPut]
        [Route("CreateStation")]
        public Task<IResponse> CreateStation(ulong id, bool enable, string? name, string? adress, double lat, double Long){
        return yql.CreateStation(id,enable,name,adress,lat,Long);
        }
            
        [HttpGet]
        [Route("GetStation")]
        public string GetStation(ulong id){
            return yql.GetStation(id);
        }
        
    }

