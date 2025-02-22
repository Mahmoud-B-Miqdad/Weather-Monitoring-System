
namespace WeatherMonitoringSystem.Bots
{
    public interface IWeatherBot
    {
        string Activate(double temperature, double humidity);
    }
}

