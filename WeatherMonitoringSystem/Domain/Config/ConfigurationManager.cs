
using WeatherMonitoringSystem.Config;
using Newtonsoft.Json;

namespace WeatherMonitoringSystem.Domain.Config;

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
}
