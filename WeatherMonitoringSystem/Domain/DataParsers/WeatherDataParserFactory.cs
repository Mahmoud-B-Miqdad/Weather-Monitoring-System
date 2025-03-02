namespace WeatherMonitoringSystem.DataParsers;

public class WeatherDataParserFactory
{
    private IWeatherDataParser _currentParser;

    public void SetParser(string inputFormat)
    {
        _currentParser = inputFormat.ToLower() switch
        {
            "json" => new JsonWeatherDataParser(),
            "xml" => new XmlWeatherDataParser(),
            _ => throw new ArgumentException("Unsupported data format.")
        };
    }

    public IWeatherDataParser GetParser()
    {
        return _currentParser ?? throw new InvalidOperationException("Parser has not been set.");
    }
}
