using WeatherMonitoringSystem.Bots;

namespace WeatherMonitoringSystem.Core
{
    public class WeatherStation
    {
        private List<IWeatherBot> _bots = new();
        private WeatherData _currentWeather;

        public void AddBot(IWeatherBot bot)
        {
            _bots.Add(bot);
        }

        public void RemoveBot(IWeatherBot bot)
        {
            _bots.Remove(bot);
        }

        public List<(IWeatherBot bot, string message)> SetWeatherData(WeatherData data)
        {
            _currentWeather = data;
            return NotifyBots();
        }

        private List<(IWeatherBot bot, string message)> NotifyBots()
        {
            List<(IWeatherBot, string)> activatedBots = new List<(IWeatherBot, string)>();
            string botMessage;

            foreach (var bot in _bots)
            {
                bool isActivated = bot.Update(_currentWeather, out botMessage);

                if (isActivated)
                {
                    activatedBots.Add((bot, botMessage));
                }
            }

            return activatedBots;
        }


    }
}
