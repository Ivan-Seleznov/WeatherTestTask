using System.Net;

namespace WeatherTestTask.Application.Common.Exceptions;

public class NotFoundException(string title, string message, string? type = null)
    : RequestException(HttpStatusCode.NotFound, title, message, type);
