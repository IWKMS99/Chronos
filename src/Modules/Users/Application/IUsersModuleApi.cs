namespace Chronos.Modules.Users.Application;

public record UserDto(Guid Id, string Email, string Role);

public interface IUsersModuleApi {
    Task<UserDto?> GetUserByIdAsync(Guid userId);
}