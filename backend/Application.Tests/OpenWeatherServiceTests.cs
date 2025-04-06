using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using WeatherTestTask.Application.Common.Exceptions;
using WeatherTestTask.Application.Common.Models;
using WeatherTestTask.Application.Common.Models.Dtos;
using WeatherTestTask.Infrastructure.Services;

namespace Application.Tests;

public class OpenWeatherServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IConfigurationSection> _weatherApiSectionMock;
    private readonly Mock<IConfigurationSection> _apiKeySectionMock;
    private readonly OpenWeatherService _service;
    private readonly HttpClient _httpClient;

    public OpenWeatherServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        _configurationMock = new Mock<IConfiguration>();
        _weatherApiSectionMock = new Mock<IConfigurationSection>();
        _apiKeySectionMock = new Mock<IConfigurationSection>();

        _configurationMock.Setup(x => x.GetSection("WeatherApi")).Returns(_weatherApiSectionMock.Object);
        _weatherApiSectionMock.Setup(x => x.GetSection("ApiKey")).Returns(_apiKeySectionMock.Object);
        _weatherApiSectionMock.Setup(x => x.Value).Returns("value");
        _apiKeySectionMock.Setup(x => x.Value).Returns("test-api-key");

        _service = new OpenWeatherService(_httpClient, _configurationMock.Object);
    }

    private void SetupHttpResponse(HttpStatusCode statusCode, object? content = null)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = content == null ? null : JsonContent.Create(content)
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }

    [Fact]
    public async Task GetWeatherAsync_ValidCity_ReturnsWeatherDto()
    {
        // Arrange
        var city = "Mykolaiv";
        var expectedResponse = new WeatherResponse
        {
            Name = city,
            Main = new Main { Temp = 15.5, Feels_Like = 14.2, Humidity = 65 },
            Weather = [new() { Description = "Cloudy" }]
        };
        
        SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

        // Act
        var result = await _service.GetWeatherAsync(city);

        // Assert
        AssertWeatherDto(result, city, 15.5, 14.2, "Cloudy", 65);
    }

    [Fact]
    public async Task GetWeatherAsync_CityNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var city = "NonexistentCity";
        SetupHttpResponse(HttpStatusCode.NotFound);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetWeatherAsync(city));
    }

    private void AssertWeatherDto(WeatherDto result, string city, double temperature, double feelsLike, string description, int humidity)
    {
        Assert.Equal(city, result.City);
        Assert.Equal(temperature, result.Temperature);
        Assert.Equal(feelsLike, result.FeelsLike);
        Assert.Equal(description, result.WeatherDescription);
        Assert.Equal(humidity, result.Humidity);
    }
}
