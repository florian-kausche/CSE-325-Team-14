namespace StudentProjectPlanner.Models;

/// <summary>
/// Configuration options for OpenWeatherMap API integration.
/// These settings are read from appsettings.json under the "OpenWeatherMap" section.
/// Provides API key, base URL, and default city for weather data retrieval.
/// </summary>
public class OpenWeatherMapOptions
{
    /// <summary>
    /// The configuration section name for OpenWeatherMap settings
    /// </summary>
    public const string SectionName = "OpenWeatherMap";

    /// <summary>
    /// API key for authenticating requests to OpenWeatherMap API
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base URL for OpenWeatherMap API endpoints
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.openweathermap.org/data/2.5/";

    /// <summary>
    /// Default city to use when fetching weather data if no city is specified
    /// </summary>
    public string DefaultCity { get; set; } = "Accra";
}
