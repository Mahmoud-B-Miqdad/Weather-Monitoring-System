using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

public class WeatherParserStrategy
{
    private IWeatherDataParser _parser;

    public void SetParser(IWeatherDataParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }
}
