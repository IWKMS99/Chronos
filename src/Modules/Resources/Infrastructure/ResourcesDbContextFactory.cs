using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Chronos.Resources.Infrastructure;

public class ResourcesDbContextFactory : IDesignTimeDbContextFactory<ResourcesDbContext> {
    public ResourcesDbContext CreateDbContext(string[] args) {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../src/Api"))
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres");
        
        var optionsBuilder = new DbContextOptionsBuilder<ResourcesDbContext>();
        optionsBuilder.UseNpgsql(connectionString,
            o => o.MigrationsAssembly(typeof(ResourcesDbContext).Assembly.FullName)); 

        return new ResourcesDbContext(optionsBuilder.Options);
    }
}