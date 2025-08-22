public record RegisterUserRequest(string Email, string Password);

public record RegisterUserCommand(string Email, string Password);