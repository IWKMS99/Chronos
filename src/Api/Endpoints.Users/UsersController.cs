using System.Security.Claims;
using Chronos.Users.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Endpoints.Users;

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase {
    private readonly UserService _userService;

    public UsersController(UserService userService) {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
        await _userService.RegisterAsync(request.Email, request.Password);
        
        return StatusCode(201);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        var token = await _userService.LoginAsync(request.Email, request.Password);
        return Ok(token);
    }
    
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser() {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        if (userId is null || userEmail is null) {
            return Unauthorized();
        }
        
        return Ok(new {
            Id = userId,
            Email = userEmail
        });
    }
}