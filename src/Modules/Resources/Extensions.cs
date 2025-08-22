using Chronos.Resources.Application;
using Chronos.Resources.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Resources;

public static class Extensions {
    public static IServiceCollection AddResourcesModule(this IServiceCollection services, string connectionString) {
        services.AddScoped<ResourceService>();
        services.AddDbContext<ResourcesDbContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}