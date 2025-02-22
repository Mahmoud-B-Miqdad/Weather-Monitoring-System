
using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.DataParsers
{
    public interface IWeatherDataParser
    {
        WeatherData Parse(string inputData);
    }
}
