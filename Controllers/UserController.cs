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
using ProjectTestAPI_1.Models;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private UserYQL yql;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            yql = new UserYQL(client);
            _logger = logger;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public string RegisterUser(
            [Required()]ulong id,
            [Required()]string name,
            [Required()]string email,
            [Required()]string password,
            [Required()]string phone)
        {
            var userGUID = Guid.NewGuid();
            return yql.RegisterUser(id,name,email,password,phone,userGUID.ToString());
        }
        [HttpGet]
        [Route("LoginUser")]
        public string LoginUser(
            [Required()]string email,
            [Required()]string password)
        {
            return yql.LoginUser(email,password);
        }
        [HttpGet]
        [Route("GetUserSettingsFromToken")]
        public string GetUserSettingsFromToken(
            [Required()]string token)
        {
            return yql.GetUserSettingsFromToken(token);
        }
        [HttpPost]
        [Route("ChangeUserSettings")]
        public Task<IResponse> ChangeUserSettings(
            [Required()]string token,
            [Required()]string name,
            [Required()]string email,
            [Required()]string fuelType,
            [Required()]string fuelSize)
        {
            return yql.ChangeUserSettings(token,name,email,fuelType,fuelSize);
        }
    }
}