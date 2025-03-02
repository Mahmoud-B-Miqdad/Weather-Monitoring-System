
using Newtonsoft.Json;

using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.DataParsers;

public class JsonWeatherDataParser : IWeatherDataParser
{
    public WeatherData Parse(string inputData)
    {
        return JsonConvert.DeserializeObject<WeatherData>(inputData);
    }
}
