using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
namespace WeatherMonitoringSystem.Bots;

public class SnowBot : IWeatherBot
{
    public BotConfig Config { get; }
    public bool IsActivated { get; set; }
    public string Message { get; private set; } = string.Empty;

    public SnowBot(BotConfig config)
    {
        Config = config ?? throw new ArgumentNullException(nameof(config));
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

