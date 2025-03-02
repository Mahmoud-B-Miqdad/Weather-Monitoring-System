
using System.Xml.Serialization;
using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.DataParsers;

public class XmlWeatherDataParser : IWeatherDataParser
{
    public WeatherData Parse(string inputData)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(WeatherData));
            using var reader = new StringReader(inputData);
            return (WeatherData)serializer.Deserialize(reader);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing XML: {ex.Message}");
        }
    }
}