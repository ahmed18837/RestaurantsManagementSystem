using Restaurants.API.Extensions;
using Restaurants.API.MiddleWares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor(); // ?? „„ «“

var app = builder.Build();

// Middlewares
app.UseRouting(); // ﬁ»· Authentication/Authorization

app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

// ? «· — Ì» «·„Â„ Â‰«:
app.UseAuthentication();    // √Ê·«
app.UseAuthorization();     // »⁄œÂ

app.MapControllers();

app.Run();
