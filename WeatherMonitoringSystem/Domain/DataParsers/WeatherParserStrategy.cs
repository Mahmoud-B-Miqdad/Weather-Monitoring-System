using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

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
        catch (Exception ex)
        {
            throw new Exception($"Error parsing JSON/XML: {ex.Message}");
        }
    }
}
