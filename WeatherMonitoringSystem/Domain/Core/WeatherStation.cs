using WeatherMonitoringSystem.Bots;

namespace WeatherMonitoringSystem.Core
{
    public class WeatherStation
    {
        private List<IWeatherBot> _bots = new();
        private WeatherData _currentWeather;

        public void AddBot(IWeatherBot bot)
        {
            if(bot == null)
                throw new ArgumentNullException("Invalid bot");

            _bots.Add(bot);
        }

        public void RemoveBot(IWeatherBot bot)
        {
            if (bot == null)
                throw new ArgumentNullException(nameof(bot));

            _bots.Remove(bot);
        }

        public List<IWeatherBot> GetAllBots()
        {
            return _bots;
        }

        public List<IWeatherBot> SetWeatherData(WeatherData data)
        {
            _currentWeather = data;
            ActivateBots();
            return GetActivatedBots();
        }

        private void ActivateBots()
        {
            foreach (var bot in _bots)
            {
                bot.Trigger(_currentWeather);
            }
        }

        private List<IWeatherBot> GetActivatedBots()
        {
            return _bots.Where(bot => bot.IsActivated).ToList();
        }
    }
}
