using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherTestTask.Application.Common.Exceptions;

namespace WeatherTestTask.Web.ExceptionHandlers;

public abstract class BaseExceptionHandler : IExceptionHandler
{
    public abstract ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);
    
    protected virtual async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var title = "Something went wrong";
        var type = "about:blank";
        var detail = exception.Message;

        if (exception is RequestException requestException)
        {
            statusCode = requestException.StatusCode;
            title = requestException.Title;
            type = requestException.Type;
            detail = requestException.Message;
        }

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = (int)statusCode,
            Type = type,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = problemDetails.Status ?? (int)statusCode;

        await JsonSerializer.SerializeAsync(httpContext.Response.Body, problemDetails);
    }
}