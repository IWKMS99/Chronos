public record LoginRequest(string Email, string Password);

public record TokenResponse(string AccessToken, string RefreshToken);

public record LoginCommand(string Email, string Password);

// TODO: Добавить обработчик (Handler) для этой команды.