using WeatherTestTask.Application.Common.Exceptions;

namespace WeatherTestTask.Infrastructure.Exceptions;

public static class WeatherServiceExceptions
{
    public static NotFoundException NotFound(string city) 
        => new NotFoundException("Weather not found", $"Weather for {city} was not found.");
}