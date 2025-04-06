using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherTestTask.Application.Common.Interfaces;
using WeatherTestTask.Infrastructure.Data;
using WeatherTestTask.Infrastructure.Services;

namespace WeatherTestTask.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IWeatherService, OpenWeatherService>(client =>
        {
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        });

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IWeatherQueryHistoryService, WeatherQueryHistoryService>();
        
        return services;
    }
}