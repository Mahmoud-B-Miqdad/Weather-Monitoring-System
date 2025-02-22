using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMonitoringSystem.Config;

namespace WeatherMonitoringSystem.Domain.Config
{
    using Newtonsoft.Json;
    public class ConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static readonly object _lock = new object();

        public BotConfig RainBot { get; private set; }
        public BotConfig SunBot { get; private set; }
        public BotConfig SnowBot { get; private set; }

        private ConfigurationManager()
        {
            LoadConfiguration();
        }

        public static ConfigurationManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new ConfigurationManager();
                }
            }
        }

        private void LoadConfiguration()
        {
            try
            {
                string relativePath = @"JSON_File\botConfig.json";
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }

                string json = File.ReadAllText(configPath);
                dynamic config = JsonConvert.DeserializeObject<dynamic>(json);

                RainBot = JsonConvert.DeserializeObject<BotConfig>(config["RainBot"].ToString());
                SunBot = JsonConvert.DeserializeObject<BotConfig>(config["SunBot"].ToString());
                SnowBot = JsonConvert.DeserializeObject<BotConfig>(config["SnowBot"].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
            }
        }
    }
}
