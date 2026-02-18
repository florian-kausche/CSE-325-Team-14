using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Service for retrieving current weather data from OpenWeatherMap.
/// </summary>
public class OpenWeatherMapService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapOptions _options;

    public OpenWeatherMapService(HttpClient httpClient, IOptions<OpenWeatherMapOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }
    }

    public async Task<WeatherSummary?> GetCurrentWeatherAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            throw new InvalidOperationException("OpenWeatherMap API key is not configured.");
        }

        var requestUri = $"weather?q={Uri.EscapeDataString(city)}&appid={_options.ApiKey}&units=metric";
        var response = await _httpClient.GetFromJsonAsync<OpenWeatherMapResponse>(requestUri);

        if (response == null || response.Main == null)
        {
            return null;
        }

        var weather = response.Weather?.FirstOrDefault();

        return new WeatherSummary
        {
            City = string.IsNullOrWhiteSpace(response.Name) ? city : response.Name,
            TemperatureC = response.Main.Temp,
            Description = weather?.Description ?? string.Empty,
            Icon = weather?.Icon
        };
    }

    private sealed class OpenWeatherMapResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("main")]
        public MainInfo? Main { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherInfo>? Weather { get; set; }
    }

    private sealed class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }

    private sealed class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }
}
