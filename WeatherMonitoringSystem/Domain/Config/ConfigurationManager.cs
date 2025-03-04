using WeatherMonitoringSystem.Config;
using Newtonsoft.Json;

namespace WeatherMonitoringSystem.Domain.Config;

public class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> _instance = new(() => new ConfigurationManager());

    public BotConfig RainBot { get; private set; }
    public BotConfig SunBot { get; private set; }
    public BotConfig SnowBot { get; private set; }

    public static ConfigurationManager Instance => _instance.Value;

    private ConfigurationManager()
    {
        LoadConfiguration();
    }

    private void LoadConfiguration()
    {
        string relativePath = @"JSON_File\botConfig.json";
        string configPath = Path.Combine(Environment.CurrentDirectory, relativePath);

        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Configuration file not found: {configPath}");
        }

        string json = File.ReadAllText(configPath);

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new Exception("Configuration file is empty.");
        }

        var config = JsonConvert.DeserializeObject<BotsConfiguration>(json)
                     ?? throw new Exception("Failed to deserialize configuration file.");

        RainBot = config.RainBot ?? throw new Exception("RainBot configuration is missing.");
        SunBot = config.SunBot ?? throw new Exception("SunBot configuration is missing.");
        SnowBot = config.SnowBot ?? throw new Exception("SnowBot configuration is missing.");
    }
}
