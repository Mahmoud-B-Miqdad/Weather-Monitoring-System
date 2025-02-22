
namespace WeatherMonitoringSystem.Bots
{
    public interface IWeatherBot
    {
        void Activate(double temperature, double humidity);
    }
}

