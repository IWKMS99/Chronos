using Chronos.Modules.Users.Application.Abstractions;
using Chronos.Modules.Users.Application.Exceptions;
using Chronos.Modules.Users.Application.Services;
using Chronos.Modules.Users.Domain;
using MediatR;

public record RegisterUserRequest(string Email, string Password);

public record RegisterUserCommand(string Email, string Password);

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand> {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser is not null) {
            throw new UserAlreadyExistsException(request.Email);
        }

        var passwordHash = _passwordHasher.Hash(request.Password);
        var user = User.Create(request.Email, passwordHash);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}