
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

namespace WeatherMonitoringSystem.UI;

public static class UserInteraction
{
    public static void Run()
    {
        PrintMessage("Welcome to the Real-Time Weather Monitoring System!");

        WeatherDataParserFactory parserFactory = new WeatherDataParserFactory();

        while (true)
        {
            string format = GetDataFormat();

            try
            {
                parserFactory.SetParser(format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            IWeatherDataParser parser = parserFactory.GetParser();
            WeatherData weatherData = GetWeatherData(parser);
            if (weatherData == null) return;

            ActivateBots(weatherData);

            Console.WriteLine("Do you want to change the parsing method? (yes/no)");
            string change = Console.ReadLine().Trim().ToLower();
            if (change != "yes") break;
        }
    }

    private static string GetDataFormat()
    {
        return GetUserInput("Enter data format (JSON/XML): ").Trim().ToLower();
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
