
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectTestAPI_1.Services;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHostedService<MyYDBService>();

        builder.WebHost.ConfigureServices((context, services) =>
        {
            HostConfig.CertPath = context.Configuration["CertPath"];
            HostConfig.CertPassword = context.Configuration["CertPassword"];
        });

        var host = Dns.GetHostEntry("myserver.io");

        builder.WebHost.ConfigureKestrel(opt =>
        {
            /*opt.Listen(host.AddressList[0],7241, listOpt => {
            listOpt.UseHttps(HostConfig.CertPath,HostConfig.CertPassword);
                    });*/

            opt.ListenAnyIP(7241, listOpt => { listOpt.UseHttps(HostConfig.CertPath, HostConfig.CertPassword); });
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("https://mckoog.github.io",
                                                    "http://localhost",
                                                    "https://myserver.io")
                                                            .AllowCredentials()
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                              });
        });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>{
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    new List<string>()
                },
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateActor = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                ValidAudience = builder.Configuration["Jwt:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                            };
                        });
                        MyJwt.issuer = builder.Configuration["Jwt:Issuer"];
                        MyJwt.audience = builder.Configuration["Jwt:Audience"];
                        MyJwt.key = builder.Configuration["Jwt:Key"];

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();

        app.UseAuthentication();

        app.MapControllers();

        app.Run();

        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        
    }
}

public static class HostConfig
{
    public static string CertPath { get; set; }
    public static string CertPassword { get; set; }

}
public static class MyJwt
{
    public static string issuer { get; set; }
    public static string audience { get; set; }
    public static string key { get; set; }
}