using Microsoft.EntityFrameworkCore;
using WeatherTestTask.Application.Common.Interfaces;
using WeatherTestTask.Application.Common.Models.Dtos;
using WeatherTestTask.Application.Common.PagedList;
using WeatherTestTask.Domain.Entities;
using WeatherTestTask.Infrastructure.Common;
using WeatherTestTask.Infrastructure.Data;

namespace WeatherTestTask.Infrastructure.Services;

public class WeatherQueryHistoryService : IWeatherQueryHistoryService
{
    private readonly ApplicationDbContext _context;
    public WeatherQueryHistoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveWeatherQueryAsync(WeatherQueryEntity weatherQuery)
    {
        _context.WeatherQueries.Add(weatherQuery);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedList<WeatherQueryEntityDto>> GetHistoryAsync(string id, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _context.WeatherQueries
            .Where(q => q.SessionId == id)
            .OrderByDescending(q => q.DateRequested)
            .Select(h => new WeatherQueryEntityDto
            {
                DateRequested = DateTime.Now,
                City = h.City,
                Temperature = h.Temperature,  
                WeatherDescription = h.WeatherDescription, 
            });

        return await PagedListFactory.CreateWithQueryAsync(query, page, pageSize, cancellationToken);
    }
}