namespace WeatherTestTask.Application.Common.Models.Dtos;

public class WeatherQueryEntityDto
{
    public DateTime DateRequested { get; set; }

    public string City { get; set; }
    public double Temperature { get; set; }
    public string WeatherDescription { get; set; }
}