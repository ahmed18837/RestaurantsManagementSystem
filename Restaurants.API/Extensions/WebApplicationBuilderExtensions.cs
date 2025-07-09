using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurants.API.MiddleWares;
using Restaurants.Domain.Constants;
using System.Text;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();

            builder.Services.AddControllers();

            builder.Services.AddHttpContextAccessor();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddSwaggerGen(c =>
            {       // To Show Authorize Button
                c.AddSecurityDefinition("bearerAuthorization", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http, // Because Use JWT Token Bearer
                    Scheme = "Bearer"
                });
                // To Apply Token In Swagger
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuthorization"}
                    },
                    []
                }

            });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

            //builder.Host.UseSerilog((context, configuration) =>
            //    configuration.ReadFrom.Configuration(context.Configuration)
            //);

            // Manual Serilog

            //builder.Host.UseSerilog((context, configuration) =>
            //            configuration
            //            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
            //            .WriteTo.File("Logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            //            .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
            //);
        }
    }
}
