
using WeatherMonitoringSystem.Core;
using static System.Collections.Specialized.BitVector32;

namespace WeatherMonitoringSystem.Bots;

public static class BotFactory
{
    public static List<IWeatherBot> CreateBots()
    {
        var bots = new List<IWeatherBot>
            {
                new RainBot(),
                new SunBot(),
                new SnowBot()
            };

        return bots;
    }
}