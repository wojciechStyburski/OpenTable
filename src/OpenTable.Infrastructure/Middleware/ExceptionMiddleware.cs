﻿namespace OpenTable.Infrastructure.Middleware;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private record Error(string Code, string Reason);

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException => 
            (
                StatusCodes.Status400BadRequest, new Error
                (
                    exception.GetType().Name.Underscore().Replace("Exception", string.Empty), exception.Message
                )
            ),
            _ => 
            (
                StatusCodes.Status500InternalServerError, new Error
                (
                    "Error", "Internal Server Error"
                )
            )
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}