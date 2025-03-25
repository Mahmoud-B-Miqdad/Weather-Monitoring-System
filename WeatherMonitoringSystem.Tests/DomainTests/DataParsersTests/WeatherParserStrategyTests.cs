using System.Xml;
using AutoFixture;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using WeatherMonitoringSystem.Core;
using WeatherMonitoringSystem.DataParsers;

public class WeatherParserStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly WeatherParserStrategy _strategy;
    private readonly Mock<IWeatherDataParser> _parserMock;

    public WeatherParserStrategyTests()
    {
        _strategy = new WeatherParserStrategy();
        _parserMock = new Mock<IWeatherDataParser>();
    }

    [Fact]
    public void SetParser_ShouldSetParserCorrectly_ByDefault()
    {
        _strategy.SetParser(_parserMock.Object);

        var weatherData = _fixture.Create<WeatherData>();
        _parserMock.Setup(p => p.Parse(It.IsAny<string>())).Returns(weatherData);

        var result = _strategy.GetWeatherData("{}");

        result.Should().Be(weatherData);
    }

    [Fact]
    public void SetParser_ShouldThrowException_WhenNull()
    {
        Action act = () => _strategy.SetParser(null);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'parser')");
    }

    [Fact]
    public void GetWeatherData_ShouldThrowException_WhenParserNotSet()
    {
        Action act = () => _strategy.GetWeatherData("{}");

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Parser has not been set.");
    }

    [Fact]
    public void GetWeatherData_ShouldReturnParsedData()
    {
        var weatherData = _fixture.Create<WeatherData>();
        _parserMock.Setup(p => p.Parse(It.IsAny<string>())).Returns(weatherData);

        _strategy.SetParser(_parserMock.Object);
        var result = _strategy.GetWeatherData("{}");

        result.Should().Be(weatherData);
    }

    [Fact]
    public void GetWeatherData_ShouldThrowJsonException_OnInvalidJson()
    {
        _parserMock.Setup(p => p.Parse(It.IsAny<string>())).Throws(new JsonException("Invalid JSON"));

        _strategy.SetParser(_parserMock.Object);

        Action act = () => _strategy.GetWeatherData("{ invalid json }");

        act.Should().Throw<JsonException>()
            .WithMessage("Error parsing JSON: Invalid JSON");
    }

    [Fact]
    public void GetWeatherData_ShouldThrowXmlException_OnInvalidXml()
    {
        _parserMock.Setup(p => p.Parse(It.IsAny<string>())).Throws(new XmlException("Invalid XML"));

        _strategy.SetParser(_parserMock.Object);

        Action act = () => _strategy.GetWeatherData("<invalid>");

        act.Should().Throw<XmlException>()
            .WithMessage("Error parsing XML: Invalid XML");
    }
}