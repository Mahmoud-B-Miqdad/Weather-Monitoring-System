
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Config;

namespace WeatherMonitoringSystem.Bots;

public interface IWeatherBot
{
    BotConfig Config { get; }  
    bool IsActivated { get; set; }
    string Message { get; }

    bool Trigger(WeatherData data);

}

