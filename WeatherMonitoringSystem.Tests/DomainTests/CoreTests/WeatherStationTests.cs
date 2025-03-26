using Moq;
using FluentAssertions;
using AutoFixture;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.Bots;

public class WeatherStationTests
{
    private readonly Fixture _fixture = new();
    private readonly WeatherStation _weatherStation;

    public WeatherStationTests()
    {
        _weatherStation = new WeatherStation();
    }

    [Fact]
    public void AddBot_ShouldAddBotToList()
    {
        var botMock = new Mock<IWeatherBot>();

        _weatherStation.AddBot(botMock.Object);

        _weatherStation.GetAllBots().Should().ContainSingle().Which.Should().Be(botMock.Object);
    }

    [Fact]
    public void AddBot_ShouldThrowException_WhenBotIsNull()
    {
        Action act = () => _weatherStation.AddBot(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RemoveBot_ShouldDecreaseBotCount_ByDefault()
    {
        var botMock1 = new Mock<IWeatherBot>();
        var botMock2 = new Mock<IWeatherBot>();
        _weatherStation.AddBot(botMock1.Object);
        _weatherStation.AddBot(botMock2.Object);

        var countBefore = _weatherStation.GetAllBots().Count;

        _weatherStation.RemoveBot(botMock1.Object);

        var countAfter = _weatherStation.GetAllBots().Count;

        countBefore.Should().Be(2);
        countAfter.Should().Be(1);
    }

    [Fact]
    public void RemoveBot_ShouldThrowException_WhenBotIsNull()
    {
        Action act = () => _weatherStation.RemoveBot(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RemoveBot_ShouldNotAlterList_WhenBotIsNotInList()
    {
        var botMock = new Mock<IWeatherBot>();

        _weatherStation.RemoveBot(botMock.Object);

        _weatherStation.GetAllBots().Should().BeEmpty();
    }


    [Fact]
    public void GetAllBots_ShouldReturnAllAddedBots_ByDefault()
    {
        var botMock1 = new Mock<IWeatherBot>();
        var botMock2 = new Mock<IWeatherBot>();

        _weatherStation.AddBot(botMock1.Object);
        _weatherStation.AddBot(botMock2.Object);

        var allBots = _weatherStation.GetAllBots();

        allBots.Should().HaveCount(2).And.Contain(new[] { botMock1.Object, botMock2.Object });
    }

    [Fact]
    public void GetAllBots_ShouldReturnEmptyList_WhenNoBotsAdded()
    {
        _weatherStation.GetAllBots().Should().BeEmpty();
    }


    [Fact]
    public void SetWeatherData_ShouldTriggerAllBots_ByDefault()
    {
        var botMock1 = new Mock<IWeatherBot>();
        var botMock2 = new Mock<IWeatherBot>();
        var weatherData = _fixture.Create<WeatherData>();

        botMock1.Setup(b => b.Trigger(weatherData)).Callback(() => botMock1.SetupGet(b => b.IsActivated).Returns(true));
        botMock2.Setup(b => b.Trigger(weatherData)).Callback(() => botMock2.SetupGet(b => b.IsActivated).Returns(true));

        _weatherStation.AddBot(botMock1.Object);
        _weatherStation.AddBot(botMock2.Object);

        var activatedBots = _weatherStation.SetWeatherData(weatherData);

        botMock1.Verify(b => b.Trigger(weatherData), Times.Once);
        botMock2.Verify(b => b.Trigger(weatherData), Times.Once);
        activatedBots.Should().Contain(new[] { botMock1.Object, botMock2.Object });
    }

    [Fact]
    public void SetWeatherData_ShouldNotActivateBotsThatDoNotTrigger_ByDefault()
    {
        var botMock = new Mock<IWeatherBot>();
        botMock.SetupGet(b => b.IsActivated).Returns(false);

        _weatherStation.AddBot(botMock.Object);
        var weatherData = _fixture.Create<WeatherData>();

        var activatedBots = _weatherStation.SetWeatherData(weatherData);

        activatedBots.Should().BeEmpty();
    }
}