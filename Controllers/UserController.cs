using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using ProjectTestAPI_1.YQL;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private TableClient client = MyYDBService.Client;
        private UsersYQL yql;
        private readonly ILogger<UserController> _logger;
        private IConfiguration _config;
        public UserController(ILogger<UserController> logger,IConfiguration config)
        {
            yql = new UsersYQL(client);
            _logger = logger;
            _config = config;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public string RegisterUser(
            [Required()]ulong id,
            [Required()]string name,
            [Required()]string email,
            [Required()]string password,
            [Required()]string phone,
            [Required()]string role
            )
        {
            var userGUID = Guid.NewGuid();
            return yql.RegisterUser(id,name,email,password,phone,userGUID.ToString(),role);
        }
        [HttpGet]
        [Route("LoginUser")]
        public string LoginUser(
            [Required()]string email,
            [Required()]string password)
        {
            var loggedUser = yql.LoginUser(email,password);
            if(loggedUser != null){
            return loggedUser.Token;
            }
            else{
                return "";
            }
        }
        [HttpGet]
        [Route("AuthorizeApiUser")]
        public string AuthorizeApiUser(
            [Required()]string token)
        {
            var loggedUser = yql.GetUserAuthorizeData(token);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, loggedUser.Name),
                new Claim(ClaimTypes.MobilePhone, loggedUser.Phone),
                new Claim(ClaimTypes.Thumbprint, loggedUser.Token),
                new Claim(ClaimTypes.Role, loggedUser.Role)
            };

            var jwtToken = new JwtSecurityToken(
                issuer:  _config["Jwt:issuer"],
                audience:  _config["Jwt:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256
                )
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return tokenString;
        }
        [HttpGet]
        [Route("GetUserSettingsFromToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
        public string GetUserSettingsFromToken(
            [Required()]string token)
        {
            return yql.GetUserSettingsFromToken(token);
        }
        [HttpPost]
        [Route("ChangeUserSettings")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,standart")]
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