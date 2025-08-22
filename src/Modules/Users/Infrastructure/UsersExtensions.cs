using Chronos.Modules.Users.Application.Abstractions;
using Chronos.Modules.Users.Application.Services;
using Chronos.Modules.Users.Infrastructure.Authentication;
using Chronos.Modules.Users.Infrastructure.Persistence;
using Chronos.Modules.Users.Infrastructure.Persistence.Repositories;
using Chronos.Modules.Users.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Modules.Users.Infrastructure;

public static class UsersExtensions {
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration) {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            typeof(Chronos.Modules.Users.Application.IUsersModuleApi).Assembly));

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}