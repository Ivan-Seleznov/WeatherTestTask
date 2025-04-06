using WeatherTestTask.Application.Common.Models.Dtos;
using WeatherTestTask.Application.Common.PagedList;
using WeatherTestTask.Domain.Entities;

namespace WeatherTestTask.Application.Common.Interfaces;

/// <summary>
/// While it is generally a better practice to use a repository pattern for database interactions,
/// I have implemented this service directly for the sake of simplicity and time efficiency.
/// </summary>
public interface IWeatherQueryHistoryService
{
    Task SaveWeatherQueryAsync(WeatherQueryEntity weatherQuery);
    Task<PagedList<WeatherQueryEntityDto>> GetHistoryAsync(string id, int page, int pageSize, CancellationToken cancellationToken = default);
}