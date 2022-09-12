using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationContextInitializer>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("PoolDb"));
        }
        else
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());

        return services;
    }
}
