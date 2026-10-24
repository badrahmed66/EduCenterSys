
using EduCenterSys.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduCenterSys.API.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        logger.LogError(exception, "Unhandled exception on {Path}", httpContext.Request.Path);

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),

            ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Invalid ID Range"),

            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid Argument"),

            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),

            // Customized
            ConflictException => (StatusCodes.Status409Conflict, "Conflict"),

            DbUpdateException ex when ex.InnerException?.Message.Contains("FOREIGN KEY") == true => (StatusCodes.Status400BadRequest, "Foreign Key Constraint Failed,The referenced entity or ID does not exist."),


            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError ? "An unexpected error occured." : exception.Message,
            Instance = httpContext.Request.Path
        };


        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
        
    }
}