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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetFuel([Required()] ulong id)
        {
            return yql.GetFuel(id);
        }

    [HttpGet]
    [Route("GetFuels")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetFuels()
        {
            return yql.GetFuels();
        }
    }

}