using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Table;
using Ydb.Sdk.Client;
using ProjectTestAPI_1.YQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectTestAPI_1.Controllers;

[ApiController]
[Route("InitialDbTablesController")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
    public class AAA_InitialDbTablesController : ControllerBase
    {
    private readonly ILogger<AAA_InitialDbTablesController> _logger;
    private TableClient client = MyYDBService.Client;
    private InitialDbTablesYQL yql;

        public AAA_InitialDbTablesController(ILogger<AAA_InitialDbTablesController> logger)
        {
        _logger = logger;
        yql = new InitialDbTablesYQL(client);
        }

        [HttpPost(Name = "CreateDbTables")]
        public Task<IResponse> CreateDbTables(){
            return yql.CreateDbTables();
        }
    }