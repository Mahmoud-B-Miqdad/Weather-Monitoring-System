
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class RainBot : IWeatherBot
{
    private readonly BotConfig _config;
    private bool _isActivated;

    public RainBot()
    {
        _config = ConfigurationManager.Instance.RainBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        _isActivated = _config.Enabled && data.Humidity > _config.SensorThreshold;

        if (_isActivated)
            message = $"RainBot: \"{_config.Message}\"";

        else
            message = string.Empty;

        return _isActivated;
    }
}
