using WeatherTestTask.Application.Common.Interfaces;

namespace WeatherTestTask.Application.Common;

public class SessionIdStorage : ISessionIdStorage
{
    public string SessionId { get; set; }
}