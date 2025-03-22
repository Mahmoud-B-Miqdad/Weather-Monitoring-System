using FluentAssertions;
using WeatherMonitoringSystem.Bots;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Config;

public class SnowBotTests
{
    private readonly BotConfig _config = new BotConfig(true, 30, 0, "Snow is coming!");

    [Fact]
    public void Trigger_ShouldActivate_WhenTemperatureIsBelowThreshold()
    {
        var snowBot = new SnowBot(_config);
        var weatherData = new WeatherData("TestLocation", -5, 50);

        bool isActivated = snowBot.Trigger(weatherData);

        isActivated.Should().BeTrue();
        snowBot.Message.Should().Be("SnowBot: \"Snow is coming!\"");
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenTemperatureIsAboveThreshold()
    {
        var snowBot = new SnowBot(_config);
        var weatherData = new WeatherData("TestLocation", 5, 50);

        bool isActivated = snowBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        snowBot.Message.Should().BeEmpty();
    }

    [Fact]
    public void Trigger_ShouldNotActivate_WhenBotIsDisabled()
    {
        var config = new BotConfig(false, 0, 30, "Snow is coming!");
        var snowBot = new SnowBot(config);
        var weatherData = new WeatherData("TestLocation", -5, 50);

        bool isActivated = snowBot.Trigger(weatherData);

        isActivated.Should().BeFalse();
        snowBot.Message.Should().BeEmpty();
    }
}
