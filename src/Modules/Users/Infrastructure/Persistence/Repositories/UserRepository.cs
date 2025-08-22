using Chronos.Modules.Users.Application.Abstractions;
using Chronos.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Modules.Users.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository {
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email) {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user) {
        await _dbContext.Users.AddAsync(user);
    }
}