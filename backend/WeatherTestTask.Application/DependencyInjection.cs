using Microsoft.Extensions.DependencyInjection;
using WeatherTestTask.Application.Common;
using WeatherTestTask.Application.Common.Interfaces;

namespace WeatherTestTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISessionIdStorage, SessionIdStorage>();
        
        return services;
    }
}