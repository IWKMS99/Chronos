using Chronos.Resources.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Resources.Infrastructure;

public class ResourcesDbContext : DbContext {
    public DbSet<Resource> Resources { get; set; }

    public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("resources");

        var resource = modelBuilder.Entity<Resource>();
        resource.HasKey(r => r.Id);
        resource.Property(r => r.Name).IsRequired();
        resource.Property(r => r.Capacity).IsRequired();
    }
}