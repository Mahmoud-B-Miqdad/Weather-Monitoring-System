using System.Xml;
using System.Xml.Serialization;
using WeatherMonitoringSystem.Core;

namespace WeatherMonitoringSystem.DataParsers
{
    public class XmlWeatherDataParser : IWeatherDataParser
    {
        public WeatherData Parse(string inputData)
        {

            if (inputData is null)
            {
                throw new ArgumentNullException(nameof(inputData), "Input XML data cannot be null.");
            }

            try
            {
                var serializer = new XmlSerializer(typeof(WeatherData));
                using var reader = new StringReader(inputData);
                return (WeatherData)serializer.Deserialize(reader) ?? throw new InvalidOperationException("Failed to deserialize XML.");
            }
            catch (InvalidOperationException ex)
            {
                throw new FormatException("Invalid XML format or structure.", ex);
            }
            catch (XmlException ex)
            {
                throw new FormatException("Malformed XML content.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong. Please try again later.");
            }
        }
    }
}
