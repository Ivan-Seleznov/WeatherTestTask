using System.Net;

namespace WeatherTestTask.Application.Common.Exceptions;

public class RequestException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public string Title { get; }
    public string Type { get; }
    protected RequestException(HttpStatusCode statusCode, string title, string? message, string? type = null) : base(message)
    {
        Title = title;
        Type = string.IsNullOrEmpty(type) ? "about:blank" : type;
        
        StatusCode = statusCode;
    }
}