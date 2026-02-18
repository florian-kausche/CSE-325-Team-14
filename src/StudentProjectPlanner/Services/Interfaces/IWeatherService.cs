using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherSummary?> GetCurrentWeatherAsync(string city);
}
