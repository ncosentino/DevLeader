namespace MicrosoftTestingPlatformExamples.TUnit;

public sealed class Tests
{
    [Test]
    public void FormatWelcomeMessage_ValidInput_ExpectedOutput()
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage("Hello");
        Assert.Equals("Hello Dev Leader!", result);
    }

    [Test]
    [Arguments("Hello", "Hello Dev Leader!")]
    [Arguments("Welcome", "Welcome Dev Leader!")]
    [Arguments("Hi", "Hi Dev Leader!")]
    public void FormatWelcomeMessage_ValidInputs_ExpectedOutputs(
        string input,
        string expected)
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage(input);
        Assert.Equals(expected, result);
    }
}