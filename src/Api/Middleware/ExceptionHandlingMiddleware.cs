using System.Net;
using System.Text.Json;
using Chronos.Users.Application.Exceptions;

namespace Chronos.Api.Middleware;

public class ExceptionHandlingMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        } catch (Exception ex) {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        var (statusCode, errorResponse) = GetErrorResponse(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private static (HttpStatusCode, ErrorResponse) GetErrorResponse(Exception exception) {
        var errorResponse = new ErrorResponse(
            "An unexpected error occurred.",
            exception.GetType().Name
        );

        HttpStatusCode statusCode = exception switch {
            UserNotFoundException or InvalidPasswordException => HttpStatusCode.BadRequest,
            UserAlreadyExistsException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };
        
        if (exception is UserNotFoundException or InvalidPasswordException or UserAlreadyExistsException) {
            errorResponse = errorResponse with { Message = exception.Message };
        }

        return (statusCode, errorResponse);
    }
    
    private record ErrorResponse(string Message, string ExceptionType);
}