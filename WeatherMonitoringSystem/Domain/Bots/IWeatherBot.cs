
using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.Bots;

public interface IWeatherBot
{
    bool Update(WeatherData data, out string message);
}

