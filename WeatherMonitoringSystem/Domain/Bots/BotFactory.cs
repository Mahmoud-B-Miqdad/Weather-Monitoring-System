
namespace WeatherMonitoringSystem.Bots
{
    public static class BotFactory
    {
        public static List<IWeatherBot> CreateBots()
        {
            return new List<IWeatherBot>
            {
                new RainBot(),
                new SunBot(),
                new SnowBot()
            };
        }
    }
}
