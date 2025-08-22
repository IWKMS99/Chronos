using Chronos.Modules.Users.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Endpoints.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase {
    private readonly ISender _sender;

    public UsersController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request) {
        var command = new RegisterUserCommand(request.Email, request.Password);
        await _sender.Send(command);
        return StatusCode(201);
    }
}