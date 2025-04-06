namespace WeatherTestTask.Application.Common.Models.Dtos;

public class WeatherDto
{
    public string City { get; set; }
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public string WeatherDescription { get; set; }
    public int Humidity { get; set; }
}