namespace Chronos.Api.Middleware;

public static class Extensions {
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}