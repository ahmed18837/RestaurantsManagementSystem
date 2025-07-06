using Microsoft.OpenApi.Models;
using Restaurants.API.MiddleWares;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();

            builder.Services.AddControllers();

            builder.Services.AddHttpContextAccessor();

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
