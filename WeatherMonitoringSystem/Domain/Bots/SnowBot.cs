
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class SnowBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }
    public string Message { get; private set; } = string.Empty;


    public SnowBot()
    {
        Config = ConfigurationManager.Instance.SnowBot;
    }

    public bool Trigger (WeatherData data)
    {
        IsActivated = Config.Enabled && data.Temperature < Config.SensorThreshold;

        if (IsActivated)
            Message = $"SnowBot: \"{Config.Message}\"";

        else
            Message = string.Empty;

        return IsActivated;
    }
}

