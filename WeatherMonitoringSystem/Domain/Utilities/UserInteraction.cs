
using System.Xml;
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;
using Newtonsoft.Json;

namespace WeatherMonitoringSystem.UI;

public static class UserInteraction
{

    public static void Run()
    {
        PrintMessage("Welcome to the Real-Time Weather Monitoring System!");

        WeatherParserStrategy parserStrategy = new WeatherParserStrategy();
        IWeatherDataParser parser = null;

        while (true)
        {
            string format = GetDataFormat();

            try
            {
                parser = GetWeatherDataParser (format);
                parserStrategy.SetParser(parser);

                string inputData = GetUserInput("Enter weather data: ").Trim();

                WeatherData weatherData = parserStrategy.GetWeatherData(inputData);
                PrintMessage($"\nReceived Data: {weatherData}");
                ActivateBots(weatherData);
            }
            catch (InvalidOperationException InvOpEx)
            {
                PrintMessage($"\nFailed to process weather data.");
            }
            catch (JsonException jsonEx)
            {
                PrintMessage("There was an error parsing the JSON data. Please check the input data.");
            }
            catch (XmlException xmlEx)
            {
                PrintMessage("There was an error parsing the XML data. Please check the input data.");
            }
            catch (ArgumentNullException argNullEx)
            {
                PrintMessage("The required data was not found. Please check the settings.");
            }
            catch (ArgumentException argEx) 
            {
                PrintMessage("There was an error with the data format. Please check the input data.");
            }
            catch (Exception ex)
            {
                PrintMessage("Something went wrong. Please try again later.");
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

    private static void ActivateBots(WeatherData weatherData)
    {
        WeatherStation station = new WeatherStation();
        List<IWeatherBot> bots = BotFactory.CreateBots();
        station.AddBots(bots);
        List<IWeatherBot> activatedBots = null;
        try
        {
            activatedBots = station.SetWeatherData(weatherData);
        }
        catch(FileNotFoundException FNFEX)
        {
            PrintMessage("The specified file could not be found. Please ensure the file path is correct and try again.");
        }
        catch (Exception ex)
        {
            PrintMessage($"Error loading configuration: {ex.Message}");
        }


        PrintMessage("*****************************************");
        foreach (var bot in activatedBots)
        {
            PrintMessage($"{bot.GetType().Name} activated!");
            PrintMessage(bot.Message);
        }
        PrintMessage("*****************************************");
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
