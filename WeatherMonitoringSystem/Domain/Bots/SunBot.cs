
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class SunBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }

    public SunBot()
    {
        Config = ConfigurationManager.Instance.SunBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        IsActivated = Config.Enabled && data.Temperature > Config.SensorThreshold;

        if (IsActivated)
            message = $"SunBot: \"{Config.Message}\"";

        else
            message = string.Empty;

        return IsActivated;
    }
}
