using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Table;
using Ydb.Sdk.Client;
using ProjectTestAPI_1.YqlScript;

namespace ProjectTestAPI_1.Controllers;

[ApiController]
[Route("[controller]")]
    public class InitialDbTablesController : ControllerBase
    {
    private readonly ILogger<InitialDbTablesController> _logger;
    private TableClient client = MyYDBService.Client;
    private YQL yql;

        public InitialDbTablesController(ILogger<InitialDbTablesController> logger)
        {
        _logger = logger;
        yql = new YQL(client);
        }

        [HttpGet(Name = "CreateDbTables")]
        public Task<IResponse> CreateDbTables(){
            return yql.CreateDbTables();
        }
    }