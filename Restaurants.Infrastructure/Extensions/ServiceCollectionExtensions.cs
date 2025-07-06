using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");

            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());

            services.AddDbContext<RestaurantsDbContext>();

            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IRatingsRepository, RatingsRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddHttpContextAccessor();

        }
    }
}
