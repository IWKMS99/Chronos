using Chronos.Users.Application.Exceptions;
using Chronos.Users.Domain;
using Chronos.Users.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Users.Application;

public class UserService
{
    private readonly UsersDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtProvider _jwtProvider;

    public UserService(UsersDbContext dbContext, PasswordHasher passwordHasher, JwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task RegisterAsync(string email, string password)
    {
        var existingUser = await _dbContext.Users.AnyAsync(u => u.Email == email);
        if (existingUser)
        {
            throw new UserAlreadyExistsException(email);
        }

        var passwordHash = _passwordHasher.Hash(password);
        var user = new User(Guid.NewGuid(), email, passwordHash, "Member");

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            throw new UserNotFoundException(email);
        }

        var isPasswordValid = _passwordHasher.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new InvalidPasswordException();
        }

        var token = _jwtProvider.Generate(user);
        return token;
    }
}