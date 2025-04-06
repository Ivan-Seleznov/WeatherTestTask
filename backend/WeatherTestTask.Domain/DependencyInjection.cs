using Microsoft.Extensions.DependencyInjection;

namespace WeatherTestTask.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}