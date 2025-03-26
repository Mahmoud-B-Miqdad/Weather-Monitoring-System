using FluentAssertions;
using WeatherMonitoringSystem.DataParsers;

public class XmlWeatherDataParserTests
{
    private readonly XmlWeatherDataParser _parser;

    public XmlWeatherDataParserTests()
    {
        _parser = new XmlWeatherDataParser();
    }

    [Fact]
    public void Parse_ShouldReturnWeatherData_WhenXmlIsValid()
    {
        string validXml = "<Weather> <Temperature>25.5</Temperature> <Humidity>60</Humidity> </Weather>";

        var result = _parser.Parse(validXml);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(25.5);
        result.Humidity.Should().Be(60);
    }
    [Fact]
    public void Parse_ShouldThrowFormatException_WhenXmlIsInvalid()
    {
        string invalidXml = "<InvalidXml>";

        Action act = () => _parser.Parse(invalidXml);

        act.Should().Throw<FormatException>()
            .WithMessage("Invalid XML format or structure.*");
    }

    [Fact]
    public void Parse_ShouldThrowArgumentNullException_WhenInputIsNull()
    {
        Action act = () => _parser.Parse(null);

        act.Should().Throw<ArgumentNullException>();
    }

}
