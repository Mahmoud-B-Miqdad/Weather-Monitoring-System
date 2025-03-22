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
    public void AddBot_Should_Add_Bot_To_List()
    {
        var botMock = new Mock<IWeatherBot>();

        _weatherStation.AddBot(botMock.Object);

        _weatherStation.GetAllBots().Should().ContainSingle().Which.Should().Be(botMock.Object);
    }

    [Fact]
    public void AddBot_Should_Throw_Exception_When_Bot_Is_Null()
    {
        Action act = () => _weatherStation.AddBot(null);

        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void RemoveBot_Should_Remove_Bot_From_List()
    {
        var botMock = new Mock<IWeatherBot>();
        _weatherStation.AddBot(botMock.Object);

        _weatherStation.RemoveBot(botMock.Object);

        _weatherStation.GetAllBots().Should().BeEmpty();
    }

    [Fact]
    public void RemoveBot_Should_Throw_Exception_When_Bot_Is_Null()
    {
        Action act = () => _weatherStation.RemoveBot(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RemoveBot_Should_Not_Alter_List_When_Bot_Is_Not_In_List()
    {
        var botMock = new Mock<IWeatherBot>();

        _weatherStation.RemoveBot(botMock.Object);

        _weatherStation.GetAllBots().Should().BeEmpty();
    }


    [Fact]
    public void GetAllBots_Should_Return_All_Added_Bots()
    {
        var botMock1 = new Mock<IWeatherBot>();
        var botMock2 = new Mock<IWeatherBot>();

        _weatherStation.AddBot(botMock1.Object);
        _weatherStation.AddBot(botMock2.Object);

        var allBots = _weatherStation.GetAllBots();

        allBots.Should().HaveCount(2).And.Contain(new[] { botMock1.Object, botMock2.Object });
    }

    [Fact]
    public void GetAllBots_Should_Return_Empty_List_When_No_Bots_Added()
    {
        _weatherStation.GetAllBots().Should().BeEmpty();
    }


    [Fact]
    public void SetWeatherData_Should_Trigger_All_Bots()
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
    public void SetWeatherData_Should_Not_Activate_Bots_That_Do_Not_Trigger()
    {
        var botMock = new Mock<IWeatherBot>();
        botMock.SetupGet(b => b.IsActivated).Returns(false);

        _weatherStation.AddBot(botMock.Object);
        var weatherData = _fixture.Create<WeatherData>();

        var activatedBots = _weatherStation.SetWeatherData(weatherData);

        activatedBots.Should().BeEmpty();
    }
}