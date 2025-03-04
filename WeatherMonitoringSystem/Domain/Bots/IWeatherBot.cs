
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Config;

namespace WeatherMonitoringSystem.Bots;

public interface IWeatherBot
{
    bool Update(WeatherData data, out string message);
    BotConfig Config { get; }  
    bool IsActivated { get; set; }
}

