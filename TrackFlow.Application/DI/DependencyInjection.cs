using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackFlow.Application.Services.Implementation;
using TrackFlow.Application.Services.Interface;
using TrackFlow.Domain.Interfaces.IRepository;
using TrackFlow.Infrastructure.Repository;
using TrackFlow.Infrastructure.Setting;


namespace TrackFlow.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var minioSetting = new MinioSettings();
        config.GetSection("Minio").Bind(minioSetting);
        services.AddSingleton(minioSetting);
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        
        // Register repositories
        // services.AddScoped<IYourRepo, YourRepo>();
        services.AddScoped<IUserRepository, UserRepository>();
        
    // Register services
    // services.AddScoped<IYourService, YourService>();
    services.AddScoped<IAuthService, AuthService>();
        

        return services;
    }
}