﻿using FluentAssertions;
using System.Xml;
using WeatherMonitoringSystem.DataParsers;

public class XmlWeatherDataParserTests
{
    private readonly XmlWeatherDataParser _parser;

    public XmlWeatherDataParserTests()
    {
        _parser = new XmlWeatherDataParser();
    }

    [Fact]
    public void Parse_Should_Return_WeatherData_When_Xml_Is_Valid()
    {
        string validXml = "<Weather> <Temperature>25.5</Temperature> <Humidity>60</Humidity> </Weather>";

        var result = _parser.Parse(validXml);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(25.5);
        result.Humidity.Should().Be(60);
    }
    [Fact]
    public void Parse_Should_Throw_FormatException_When_Xml_Is_Invalid()
    {
        string invalidXml = "<InvalidXml>";

        Action act = () => _parser.Parse(invalidXml);

        act.Should().Throw<FormatException>()
            .WithMessage("Invalid XML format or structure.*");
    }

    [Fact]
    public void Parse_Should_Throw_FormatException_When_Input_Is_Empty()
    {
        string emptyXml = "";

        Action act = () => _parser.Parse(emptyXml);

        act.Should().Throw<FormatException>()
            .WithMessage("Malformed XML content.*");
    }

    [Fact]
    public void Parse_Should_Throw_ArgumentNullException_When_Input_Is_Null()
    {
        Action act = () => _parser.Parse(null);

        act.Should().Throw<ArgumentNullException>();
    }

}
