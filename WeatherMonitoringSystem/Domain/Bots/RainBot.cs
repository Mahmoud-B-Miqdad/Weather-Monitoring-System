
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

        public void Activate(double temperature, double humidity)
        {
            if (_config.Enabled && humidity > _config.SensorThreshold)
            {
                Console.WriteLine("RainBot activated!");
                Console.WriteLine($"RainBot: \"{_config.Message}\"");
            }
        }
    }
}
