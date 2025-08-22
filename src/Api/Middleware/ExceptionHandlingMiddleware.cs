using System.Net;
using System.Text.Json;
using Chronos.Modules.Users.Application.Exceptions;

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
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        var statusCode = HttpStatusCode.InternalServerError;
        object? response = null;

        switch (exception) {
            case UserAlreadyExistsException:
                statusCode = HttpStatusCode.Conflict;
                response = new { error = exception.Message };
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        if (response is not null)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}