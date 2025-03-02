
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public class SunBot : IWeatherBot
{
    private readonly BotConfig _config;
    private bool _isActivated;

    public SunBot()
    {
        _config = ConfigurationManager.Instance.SunBot;
    }

    public bool Update(WeatherData data, out string message)
    {
        _isActivated = _config.Enabled && data.Temperature > _config.SensorThreshold;

        if (_isActivated)
            message = $"SunBot: \"{_config.Message}\"";

        else
            message = string.Empty;

        return _isActivated;
    }
}
