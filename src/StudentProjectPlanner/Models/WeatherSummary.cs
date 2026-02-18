namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents a weather summary containing current weather information
/// for a specific location. Used to display weather data on the dashboard.
/// </summary>
public class WeatherSummary
{
    /// <summary>
    /// The city name for which this weather information applies
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Temperature in Celsius
    /// </summary>
    public double TemperatureC { get; set; }

    /// <summary>
    /// Description of the weather condition (e.g., "Sunny", "Rainy", "Cloudy")
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Icon code for the weather condition (used for displaying weather icons from OpenWeather)
    /// </summary>
    public string? Icon { get; set; }
}
