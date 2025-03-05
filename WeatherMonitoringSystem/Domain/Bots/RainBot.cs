
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class RainBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }
    public string Message { get; private set; } = string.Empty;
    public RainBot()
    {
        Config = ConfigurationManager.Instance.RainBot;
    }

    public bool Trigger (WeatherData data)
    {
        IsActivated = Config.Enabled && data.Humidity > Config.SensorThreshold;

        if (IsActivated)
            Message = $"RainBot: \"{Config.Message}\"";

        else
            Message = string.Empty;

        return IsActivated;
    }
}
