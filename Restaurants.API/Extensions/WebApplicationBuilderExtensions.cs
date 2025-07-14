using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurants.API.MiddleWares;
using Restaurants.Domain.Constants;
using System.Text;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Presentation‑layer registrations:
        /// ▸ MVC                 
        /// ▸ API Versioning + Explorer  
        /// ▸ JWT Auth + Identity Options
        /// ▸ Swagger (version‑aware)     
        /// ▸ Global Middlewares DI      
        /// </summary>
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            //--------------------------------------------------
            // 1) MVC
            //--------------------------------------------------
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();

            //--------------------------------------------------
            // 2) API Versioning
            //--------------------------------------------------
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader(); // /api/v1/
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";        // v1, v2, …
                options.SubstituteApiVersionInUrl = true;  // replaces {version}
            });

            //--------------------------------------------------
            // 3) JWT options
            //--------------------------------------------------
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            //--------------------------------------------------
            // 4) Authentication & Identity
            //--------------------------------------------------
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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            //--------------------------------------------------
            // 5) Swagger (version‑aware)
            //--------------------------------------------------
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuthorization", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Paste **only** the JWT here"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            { Type = ReferenceType.SecurityScheme, Id = "bearerAuthorization" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
            builder.Services.AddEndpointsApiExplorer();

            //--------------------------------------------------
            // 6) Middlewares (DI)
            //--------------------------------------------------
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            // builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        }
    }
}
