using System.Net;
using System.Net.Http.Json;
using WeatherTestTask.Application.Common.Interfaces;
using WeatherTestTask.Application.Common.Models;
using WeatherTestTask.Application.Common.Models.Dtos;
using Microsoft.Extensions.Configuration;
using WeatherTestTask.Application.Common.Exceptions;
using WeatherTestTask.Infrastructure.Exceptions;

namespace WeatherTestTask.Infrastructure.Services;

public sealed class OpenWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration
                      .GetRequiredSection("WeatherApi")
                      .GetRequiredSection("ApiKey").Value
                  ?? throw new InvalidOperationException("Configuration error: 'WeatherApi:ApiKey' is missing or empty.");
    }

    public async Task<WeatherDto> GetWeatherAsync(string city)
    {
        var url = $"weather?q={city}&appid={_apiKey}&units=metric";

        var httpResponse = await _httpClient.GetAsync(url);

        if (httpResponse.StatusCode == HttpStatusCode.NotFound)
        {
            throw WeatherServiceExceptions.NotFound(city);
        }

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get weather data: {httpResponse.ReasonPhrase}");
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<WeatherResponse>();
        if (response is null)
        {
            throw new Exception("Weather response was empty or could not be deserialized.");
        }
        
        var weatherDto = MapWeatherResponseToDto(response);
        return weatherDto;
    }
    
    WeatherDto MapWeatherResponseToDto(WeatherResponse response)
    {
        return new WeatherDto
        {
            City = response.Name,
            Temperature = response.Main.Temp,
            FeelsLike = response.Main.Feels_Like,
            WeatherDescription = response.Weather.FirstOrDefault()?.Description!,
            Humidity = response.Main.Humidity
        };
    }
}