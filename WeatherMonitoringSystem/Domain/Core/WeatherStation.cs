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

        public List<IWeatherBot> SetWeatherData(WeatherData data)
        {
            _currentWeather = data;
            return GetActivatedBots();
        }

        private List<IWeatherBot> GetActivatedBots()
        {
            List<IWeatherBot> activatedBots = new List<IWeatherBot>();

            foreach (var bot in _bots)
            {
                bool isActivated = bot.Trigger (_currentWeather);

                if (isActivated)
                {
                    activatedBots.Add((bot));
                }
            }

            return activatedBots;
        }
    }
}
