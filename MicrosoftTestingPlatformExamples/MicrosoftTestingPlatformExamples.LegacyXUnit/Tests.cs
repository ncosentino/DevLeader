using Xunit;

namespace MicrosoftTestingPlatformExamples.LegacyXUnit;

public sealed class Tests
{
    [Fact]
    public void FormatWelcomeMessage_ValidInput_ExpectedOutput()
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage("Hello");
        Assert.Equal("Hello Dev Leader!", result);
    }

    [Theory]
    [InlineData("Hello", "Hello Dev Leader!")]
    [InlineData("Welcome", "Welcome Dev Leader!")]
    [InlineData("Hi", "Hi Dev Leader!")]
    public void FormatWelcomeMessage_ValidInputs_ExpectedOutputs(
        string input,
        string expected)
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage(input);
        Assert.Equal(expected, result);
    }
}