using FluentAssertions;
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Config;

public class RainBotTests
{
    private readonly BotConfig _config = new BotConfig(true, 30, 70, "It looks like it's about to pour down!");

    [Fact]
    public void Trigger_ShouldActivate_WhenHumidityExceedsThreshold()
    {
        var rainBot = new RainBot(_config);
        var weatherData = new WeatherData("TestLocation", 25, 80);

        bool isActivated = rainBot.Trigger(weatherData);

        isActivated.Should().BeTrue();
        rainBot.Message.Should().Be("RainBot: \"It looks like it's about to pour down!\"");
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenHumidityIsBelowThreshold()
    {
        var rainBot = new RainBot(_config);
        var weatherData = new WeatherData("TestLocation", 25, 60);

        bool isActivated = rainBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        rainBot.Message.Should().BeEmpty();
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenBotIsDisabled()
    {
        var config = new BotConfig(false, 30, 70, "It looks like it's about to pour down!");
        var rainBot = new RainBot(config);
        var weatherData = new WeatherData("TestLocation", 25, 80);

        bool isActivated = rainBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        rainBot.Message.Should().BeEmpty();
    }
}