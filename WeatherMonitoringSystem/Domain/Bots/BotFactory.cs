using WeatherMonitoringSystem.Domain.Config;

namespace WeatherMonitoringSystem.Bots;

public static class BotFactory
{
    public static List<IWeatherBot> CreateBots()
    {
        var bots = new List<IWeatherBot>
            {
                new RainBot(ConfigurationManager.Instance.RainBot),
                new SunBot(ConfigurationManager.Instance.SunBot),
                new SnowBot(ConfigurationManager.Instance.SnowBot)
            };

        return bots;
    }
}