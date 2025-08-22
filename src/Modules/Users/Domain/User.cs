using Chronos.Shared.Kernel.Domain;

namespace Chronos.Modules.Users.Domain;

public class User : AggregateRoot {
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    private User() : base(Guid.NewGuid()) { }

    public User(Guid id, string email, string passwordHash, string role) : base(id) {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public static User Create(string email, string passwordHash) {
        return new User(Guid.NewGuid(), email, passwordHash, "Member");
    }

    public void SetRefreshToken(string refreshToken, DateTime expiryTime) {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }

    public void ClearRefreshToken() {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }
}