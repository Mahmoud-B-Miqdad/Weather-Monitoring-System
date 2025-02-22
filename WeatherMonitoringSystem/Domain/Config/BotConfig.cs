using System;
using System.IO;
using Newtonsoft.Json;

namespace WeatherMonitoringSystem.Config
{
    using Newtonsoft.Json;
    public class BotConfig
    {
        public bool Enabled { get; set; }
        public string Message { get; set; }

        [JsonProperty("temperatureThreshold")]
        public double? TemperatureThreshold { get; set; }

        [JsonProperty("humidityThreshold")]
        public double? HumidityThreshold { get; set; }

        public double SensorThreshold { get; private set; }

        [JsonConstructor]
        public BotConfig(bool enabled, double? temperatureThreshold, double? humidityThreshold, string message)
        {
            Enabled = enabled;
            TemperatureThreshold = temperatureThreshold;
            HumidityThreshold = humidityThreshold;
            Message = message;

            InitSensorThreshold();
        }

        private void InitSensorThreshold()
        {
            SensorThreshold = HumidityThreshold ?? TemperatureThreshold ?? 0;
        }
    }
}