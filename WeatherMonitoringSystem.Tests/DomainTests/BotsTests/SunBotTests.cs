using FluentAssertions;
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Domain.Config;
using WeatherMonitoringSystem.Config;

public class SunBotTests
{
    private readonly BotConfig _config = new BotConfig(true, 70, 30, "It's a bright and sunny day!");

    [Fact]
    public void Trigger_ShouldActivate_WhenTemperatureExceedsThreshold()
    {
        var sunBot = new SunBot(_config);
        var weatherData = new WeatherData("TestLocation", 35, 50);

        bool isActivated = sunBot.Trigger(weatherData);

        isActivated.Should().BeTrue();
        sunBot.Message.Should().Be("SunBot: \"It's a bright and sunny day!\"");
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenTemperatureIsBelowThreshold()
    {
        var sunBot = new SunBot(_config);
        var weatherData = new WeatherData("TestLocation", 25, 50);

        bool isActivated = sunBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        sunBot.Message.Should().BeEmpty();
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenBotIsDisabled()
    {
        var config = new BotConfig(false, 30, 70, "It's a bright and sunny day!");
        var sunBot = new SunBot(config);
        var weatherData = new WeatherData("TestLocation", 35, 50);

        bool isActivated = sunBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        sunBot.Message.Should().BeEmpty();
    }
}
