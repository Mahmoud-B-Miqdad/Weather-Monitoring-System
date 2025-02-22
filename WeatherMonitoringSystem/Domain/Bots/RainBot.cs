
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots
{
    public class RainBot : IWeatherBot
    {
        private readonly BotConfig _config;

        public RainBot()
        {
            _config = ConfigurationManager.Instance.RainBot;
        }

        public string Activate(double temperature, double humidity)
        {
            if (_config.Enabled && humidity > _config.SensorThreshold)
            {
                return $"RainBot activated!\nRainBot: \"{_config.Message}\"";
            }
            return "";
        }
    }
}
