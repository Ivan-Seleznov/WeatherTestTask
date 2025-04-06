using WeatherTestTask.Application.Common.Models.Dtos;

namespace WeatherTestTask.Application.Common.Interfaces;

public interface IWeatherService
{
    Task<WeatherDto> GetWeatherAsync(string city);
}