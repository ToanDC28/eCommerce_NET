using eCommerce.SharedLibrary.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Application.Interfaces;
using ProductAPI.Infrastructure.Data;
using ProductAPI.Infrastructure.Repositories;
using System.Data.Entity;

namespace ProductAPI.Infrastructure.DI;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        SharedServiceContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:ProductLog"]);
        services.AddScoped<IProductRepository, PorductPrepository>();

        return services;
    }

    public static IApplicationBuilder UseInFrastructure(this IApplicationBuilder app)
    {
        SharedServiceContainer.UseSharedPolicies(app);

        return app;
    }
    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        await SharedServiceContainer.InitializeDatabasesAsync<ProductDbContext>(services, cancellationToken);
    }
}
