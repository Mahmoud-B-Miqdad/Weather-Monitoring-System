using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;

public static class WeatherStationExtensions
{
    public static void AddBots(this WeatherStation station, List<IWeatherBot> bots)
    {
        foreach (var bot in bots)
        {
            station.AddBot(bot);
        }
    }
}