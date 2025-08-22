using Chronos.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Modules.Users.Infrastructure.Persistence;

public class UsersDbContext : DbContext {
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
    }
}