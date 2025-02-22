using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeatherMonitoringSystem.DataParsers
{
    public static class WeatherDataParserFactory
    {
        public static IWeatherDataParser GetParser(string inputFormat)
        {
            return inputFormat.ToLower() switch
            {
                "json" => new JsonWeatherDataParser(),
                "xml" => new XmlWeatherDataParser(),
                _ => throw new ArgumentException("Unsupported data format.")
            };
        }
    }
}
