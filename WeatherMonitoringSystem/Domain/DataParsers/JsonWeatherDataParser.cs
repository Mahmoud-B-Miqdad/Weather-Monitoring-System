
using Newtonsoft.Json;

using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.DataParsers
{
    public class JsonWeatherDataParser : IWeatherDataParser
    {
        public WeatherData Parse(string inputData)
        {
            try
            {
                return JsonConvert.DeserializeObject<WeatherData>(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing JSON: {ex.Message}");
            }
        }
    }
}
