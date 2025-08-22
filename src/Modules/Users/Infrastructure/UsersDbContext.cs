using Chronos.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Users.Infrastructure;

public class UsersDbContext : DbContext {
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("users");
        
        var user = modelBuilder.Entity<User>();
        user.HasKey(u => u.Id);
        user.HasIndex(u => u.Email).IsUnique();
        user.Property(u => u.Email).IsRequired();
        user.Property(u => u.PasswordHash).IsRequired();
    }
}