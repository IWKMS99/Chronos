using Chronos.Modules.Users.Domain;

namespace Chronos.Modules.Users.Application.Services;

public interface IJwtProvider {
    TokenResponse Generate(User user);
}