using angularSkinet.Core.Interfaces;
using angularSkinet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace angularSkinet.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructureExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
