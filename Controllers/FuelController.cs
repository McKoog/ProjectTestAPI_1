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
    public class FuelController : ControllerBase
    {
        private readonly ILogger<FuelController> _logger;
        private TableClient client = MyYDBService.Client;
        private FuelsYQL yql;

        public FuelController(ILogger<FuelController> logger)
        {
        _logger = logger;
        yql = new FuelsYQL(client);
        }

    [HttpPost]
    [Route("CreateFuel")]
        public Task<IResponse> CreateFuel(
            [Required()] ulong id,
            [Required()] string name,
            [Required()] string fullname
            )
        {
            return yql.CreateFuel(id,name,fullname);
        }

    [HttpGet]
    [Route("GetFuel")]
        public string GetFuel([Required()] ulong id)
        {
            return yql.GetFuel(id);
        }

    [HttpGet]
    [Route("GetFuels")]
        public string GetFuels()
        {
            return yql.GetFuels();
        }
    }

}