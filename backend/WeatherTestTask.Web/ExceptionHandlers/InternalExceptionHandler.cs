namespace WeatherTestTask.Web.ExceptionHandlers;

public class InternalExceptionHandler : BaseExceptionHandler
{
    private readonly ILogger<InternalExceptionHandler> _logger;

    public InternalExceptionHandler(ILogger<InternalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);
        await HandleExceptionAsync(httpContext, exception);

        return true;
    }
}