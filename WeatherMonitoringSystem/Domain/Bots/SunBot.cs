
using WeatherMonitoringSystem.Config;
using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots
{
    public class SunBot : IWeatherBot
    {
        private readonly BotConfig _config;

        public SunBot()
        {
            _config = ConfigurationManager.Instance.SunBot;
        }

        public void Activate(double temperature, double humidity)
        {
            if (_config.Enabled && temperature > _config.SensorThreshold)
            {
                Console.WriteLine("SunBot activated!");
                Console.WriteLine($"SunBot: \"{_config.Message}\"");
            }
        }
    }
}
