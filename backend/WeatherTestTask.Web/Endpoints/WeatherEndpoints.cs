using Microsoft.Extensions.Caching.Memory;
using WeatherTestTask.Application.Common.Interfaces;
using WeatherTestTask.Application.Common.Models.Dtos;
using WeatherTestTask.Application.Common.PagedList;
using WeatherTestTask.Domain.Entities;

namespace WeatherTestTask.Web.Endpoints;

public static class WeatherEndpoints
{
    public const int DefaultPageSize = 15;
    public static void MapWeatherEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/search/{city}", async (
                string city,
                IWeatherService weatherService, 
                IMemoryCache memoryCache, 
                IWeatherQueryHistoryService saveWeatherQueryService,
                ISessionIdStorage sessionIdStorage) =>
            {
                if (memoryCache.TryGetValue(city, out WeatherDto? cachedWeather))
                {
                    return Results.Ok(cachedWeather);
                }
    
                var weather = await weatherService.GetWeatherAsync(city);

                var weatherEntity = new WeatherQueryEntity
                {
                    SessionId = sessionIdStorage.SessionId,
                    DateRequested = DateTime.UtcNow,
                    City = weather.City,
                    Temperature = weather.Temperature,
                    WeatherDescription = weather.WeatherDescription,
                };

                await saveWeatherQueryService.SaveWeatherQueryAsync(weatherEntity);

                memoryCache.Set(city, weather, TimeSpan.FromMinutes(10));

                return Results.Ok(weather);
            })
            .WithName("GetWeather")
            .Produces<WeatherDto>();
        
        endpoints.MapGet("/history", async (
                int? page,
                int? pageSize,
                IWeatherQueryHistoryService saveWeatherQueryService,
                ISessionIdStorage sessionIdStorage) =>
        {
            var history = await saveWeatherQueryService
                .GetHistoryAsync(sessionIdStorage.SessionId, page ?? 1, pageSize ?? DefaultPageSize);
            
            return Results.Ok(history);
        })
        .WithName("GetWeatherHistory")
        .Produces<PagedList<WeatherQueryEntityDto>>();
    }
}
