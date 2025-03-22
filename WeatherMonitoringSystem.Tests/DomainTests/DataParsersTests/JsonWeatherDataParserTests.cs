﻿using FluentAssertions;
using Newtonsoft.Json;
using WeatherMonitoringSystem.DataParsers;

public class JsonWeatherDataParserTests
{
    private readonly JsonWeatherDataParser _parser;

    public JsonWeatherDataParserTests()
    {
        _parser = new JsonWeatherDataParser();
    }

    [Fact]
    public void Parse_Should_Return_WeatherData_When_JsonIsValid()
    {
        string json = "{ \"Temperature\": 25, \"Humidity\": 80 }";

        var result = _parser.Parse(json);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(25);
        result.Humidity.Should().Be(80);
    }

    [Theory]
    [InlineData("{ Temperature: 25, Humidity: ")] 
    [InlineData("{ \"Temperature\": \"invalid\" }")] 
    public void Parse_Should_Throw_JsonException_When_JsonIsInvalid(string invalidJson)
    {
        Action act = () => _parser.Parse(invalidJson);

        act.Should().Throw<JsonException>();
    }

    [Fact]
    public void Parse_Should_Handle_Missing_Fields_Correctly()
    {
        var json = "{ \"Temperature\": 30 }";

        var result = _parser.Parse(json);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(30);
        result.Humidity.Should().Be(0);
    }
}
