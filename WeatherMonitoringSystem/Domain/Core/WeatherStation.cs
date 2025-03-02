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

        public void SetWeatherData(WeatherData data)
        {
            _currentWeather = data;
            NotifyBots();
        }

        private void NotifyBots()
        {
            string botMessage;
            foreach (var bot in _bots)
            {
                bot.Update(_currentWeather,out botMessage);
            }
        }
    }
}
