
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class RainBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }
    public RainBot()
    {
        Config = ConfigurationManager.Instance.RainBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        IsActivated = Config.Enabled && data.Humidity > Config.SensorThreshold;

        if (IsActivated)
            message = $"RainBot: \"{Config.Message}\"";

        else
            message = string.Empty;

        return IsActivated;
    }
}
