
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

namespace WeatherMonitoringSystem.UI
{
    public static class UserInteraction
    {
        public static void Run()
        {
            PrintMessage("Welcome to the Real-Time Weather Monitoring System!");

            string format = GetDataFormat();
            IWeatherDataParser parser = GetParser(format);
            if (parser == null) return;

            WeatherData weatherData = GetWeatherData(parser);
            if (weatherData == null) return;

            ActivateBots(weatherData);
        }

        private static string GetDataFormat()
        {
            return GetUserInput("Enter data format (JSON/XML): ").Trim().ToLower();
        }

        private static IWeatherDataParser GetParser(string format)
        {
            try
            {
                return WeatherDataParserFactory.GetParser(format);
            }
            catch (Exception ex)
            {
                PrintMessage($"Error: {ex.Message}");
                return null;
            }
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
                List<IWeatherBot> bots = BotFactory.CreateBots();

                PrintMessage("*****************************************");
                foreach (var bot in bots)
                {                    
                    PrintMessage(bot.Activate(weatherData.Temperature, weatherData.Humidity));
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
}
