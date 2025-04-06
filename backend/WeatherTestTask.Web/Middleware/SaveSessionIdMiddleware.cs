using Microsoft.AspNetCore.Session;
using WeatherTestTask.Application.Common.Interfaces;

namespace WeatherTestTask.Web.Middleware;

public class SaveSessionIdMiddleware
{
    private readonly RequestDelegate _next;

    public SaveSessionIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionIdStorage sessionIdStorage)
    {
        var sessionId = context.Session.GetString("SessionId");

        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
            context.Session.SetString("SessionId", sessionId);
        }

        sessionIdStorage.SessionId = sessionId;

        await _next(context);
    }
}