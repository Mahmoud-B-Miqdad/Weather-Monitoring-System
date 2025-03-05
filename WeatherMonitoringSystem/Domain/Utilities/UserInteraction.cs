
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

namespace WeatherMonitoringSystem.UI;

public static class UserInteraction
{

    public static void Run()
    {
        PrintMessage("Welcome to the Real-Time Weather Monitoring System!");

        WeatherParserStrategy parserStrategy = new WeatherParserStrategy();

        while (true)
        {
            string format = GetDataFormat();

            try
            {
                IWeatherDataParser parser = GetWeatherDataParser (format);

                parserStrategy.SetParser(parser);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            string inputData = GetUserInput("Enter weather data: ").Trim();

            try
            {
                WeatherData weatherData = parserStrategy.GetWeatherData(inputData);
                PrintMessage($"\nReceived Data: {weatherData}");
                ActivateBots(weatherData);
            }
            catch (Exception ex)
            {
                PrintMessage($"\nFailed to process weather data.{ex.Message}");
            }

            PrintMessage("Do you want to change the parsing method? (yes/no)");
            string change = Console.ReadLine().Trim().ToLower();
            if (change != "yes") break;
        }
    }

    private static string GetDataFormat()
    {
        return GetUserInput("Enter data format (JSON/XML): ").Trim().ToLower();
    }

    private static IWeatherDataParser GetWeatherDataParser (string format)
    {
        IWeatherDataParser parser = format.ToLower() switch
        {
            "json" => new JsonWeatherDataParser(),
            "xml" => new XmlWeatherDataParser(),
            _ => throw new ArgumentException("Unsupported data format.")
        };
        return parser;
    }

    private static WeatherData GetWeatherData(IWeatherDataParser parser)
    {
        try
        {
            string inputData = GetUserInput("Enter weather data: ").Trim();
            WeatherData weatherData = parser.Parse(inputData);

            if (weatherData == null)
            {
                PrintMessage("\nFailed to process weather data.");
            }
            else
            {
                PrintMessage($"\nReceived Data: {weatherData}");
            }

            return weatherData;
        }
        catch(Exception ex)
        {
            PrintMessage($"Error parsing JSON/XML: {ex.Message}");
            return null;
        }
    }

    private static void ActivateBots(WeatherData weatherData)
    {
        try
        {
            WeatherStation station = new WeatherStation();

            List<IWeatherBot> bots = BotFactory.CreateBots(station);
            var activatedBots = station.SetWeatherData(weatherData);

            PrintMessage("*****************************************");
            foreach (var (bot, message) in activatedBots)
            {
                PrintMessage($"{bot.GetType().Name} activated!");
                PrintMessage(message);
            }
            PrintMessage("*****************************************");
        }
        catch (Exception ex)
        {
            PrintMessage($"Error loading configuration: {ex.Message}");
        }
    }

    public static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
