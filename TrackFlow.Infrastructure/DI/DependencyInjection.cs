using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrackFlow.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        
        // Register repositories
        // services.AddScoped<IYourRepo, YourRepo>();

        // Register services
        // services.AddScoped<IYourService, YourService>();

        return services;
    }
}