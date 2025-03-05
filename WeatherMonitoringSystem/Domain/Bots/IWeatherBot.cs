
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Config;

namespace WeatherMonitoringSystem.Bots;

public interface IWeatherBot
{
    bool Trigger(WeatherData data);
    BotConfig Config { get; }  
    bool IsActivated { get; set; }
    string Message { get; }
}

