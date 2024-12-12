using EmploymentSystem.Application.Exceptions;

namespace EmploymentSystem.API.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "An error occurred while processing the request.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        if (ex is ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                message = ex.Message,
                errors = validationEx.Errors
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            message = "An unexpected error occurred.",
            detail = ex.Message
        }));
    }
}

