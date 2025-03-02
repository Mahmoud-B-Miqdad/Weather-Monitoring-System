
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class SnowBot : IWeatherBot
{
    private readonly BotConfig _config;
    private bool _isActivated;

    public SnowBot()
    {
        _config = ConfigurationManager.Instance.SnowBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        _isActivated = _config.Enabled && data.Temperature < _config.SensorThreshold;

        if (_isActivated)
            message = $"SnowBot: \"{_config.Message}\"";

        else
            message = string.Empty;

        return _isActivated;
    }
}

