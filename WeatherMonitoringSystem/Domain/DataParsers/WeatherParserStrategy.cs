using System.Xml;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;
using Newtonsoft.Json;

public class WeatherParserStrategy
{
    private IWeatherDataParser _parser;

    public void SetParser(IWeatherDataParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public WeatherData GetWeatherData(string inputData)
    {
        if (_parser == null)
            throw new InvalidOperationException("Parser has not been set.");

        try
        {
            return _parser.Parse(inputData);
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Error parsing JSON: {ex.Message}", ex);
        }
        catch (XmlException ex)
        {
            throw new XmlException($"Error parsing XML: {ex.Message}", ex);
        }
    }
}
