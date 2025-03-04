
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class SnowBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }

    public SnowBot()
    {
        Config = ConfigurationManager.Instance.SnowBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        IsActivated = Config.Enabled && data.Temperature < Config.SensorThreshold;

        if (IsActivated)
            message = $"SnowBot: \"{Config.Message}\"";

        else
            message = string.Empty;

        return IsActivated;
    }
}

