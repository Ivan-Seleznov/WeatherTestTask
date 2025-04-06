namespace WeatherTestTask.Domain.Entities;

public class WeatherQueryEntity
{
    public int Id { get; set; }
    public string SessionId { get; set; }
    public DateTime DateRequested { get; set; }

    public string City { get; set; }
    public double Temperature { get; set; }
    public string WeatherDescription { get; set; }
}