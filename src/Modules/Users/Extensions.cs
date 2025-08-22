using Chronos.Users.Application;
using Chronos.Users.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Users;

public static class Extensions {
    public static IServiceCollection AddUsersModule(this IServiceCollection services, string connectionString) {
        services.AddSingleton<PasswordHasher>();
        services.AddSingleton<JwtProvider>();
        services.AddScoped<UserService>();
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}