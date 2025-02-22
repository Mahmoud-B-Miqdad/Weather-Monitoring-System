using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace WeatherMonitoringSystem.Core
{
    [XmlRoot("Weather")]
    public class WeatherData
    {
        [XmlElement("Location")]
        public string Location { get; set; }

        [XmlElement("Temperature")]
        public double Temperature { get; set; }

        [XmlElement("Humidity")]
        public double Humidity { get; set; }

        public WeatherData() { }

        public WeatherData(string location, double temperature, double humidity)
        {
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
        }

        public override string ToString()
        {
            return $"Location: {Location}, Temperature: {Temperature}°C, Humidity: {Humidity}%";
        }
    }
}