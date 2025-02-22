
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots
{
    public class SnowBot : IWeatherBot
    {
        private readonly BotConfig _config;

        public SnowBot()
        {
            _config = ConfigurationManager.Instance.SnowBot;
        }

        public void Activate(double temperature, double humidity)
        {
            if (_config.Enabled && temperature < _config.SensorThreshold)
            {
                Console.WriteLine("SnowBot activated!");
                Console.WriteLine($"SnowBot: \"{_config.Message}\"");
            }
        }
    }
}

