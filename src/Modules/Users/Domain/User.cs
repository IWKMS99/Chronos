namespace Chronos.Users.Domain;

public class User {
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }

    private User() { }

    public User(Guid id, string email, string passwordHash, string role) {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}