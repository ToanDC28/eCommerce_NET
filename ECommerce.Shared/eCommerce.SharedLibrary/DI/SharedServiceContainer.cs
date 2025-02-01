using eCommerce.SharedLibrary.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace eCommerce.SharedLibrary.DI;

public static class SharedServiceContainer
{
    public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string filename) where TContext : DbContext
    {

        //Add Generic database context interface
        services.AddDbContext<TContext>(option => option.UseNpgsql(config.GetConnectionString("eCommerceConnection"), b => b.MigrationsAssembly("ProductAPI.Infrastructure")));

        //Configure Serilog logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(path: $"{filename}-.text", 
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, 
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day).CreateLogger();

        // Add JWT authentication scheme
        AuthenticationScheme.AddJwtAuthenticationScheme(services, config);

        return services;
    }

    public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
    {
        // Use Global Exception
        app.UseMiddleware<GlobalException>();

        // Register middleware to block all outsider api
        //app.UseMiddleware<ListentToOnlyApiGateway>();



        return app;

    }

    public static async Task InitializeDatabasesAsync<TContext>(this IServiceProvider services, CancellationToken cancellationToken = default) where TContext : DbContext
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
    }
}
