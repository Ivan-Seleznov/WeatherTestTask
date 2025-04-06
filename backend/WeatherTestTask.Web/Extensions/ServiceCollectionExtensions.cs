using WeatherTestTask.Web.ExceptionHandlers;

namespace WeatherTestTask.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<RequestExceptionHandler>();
        services.AddExceptionHandler<InternalExceptionHandler>();
    }
}