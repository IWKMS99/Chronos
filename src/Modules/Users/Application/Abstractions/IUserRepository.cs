using Chronos.Modules.Users.Domain;

namespace Chronos.Modules.Users.Application.Abstractions;

public interface IUserRepository {
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}