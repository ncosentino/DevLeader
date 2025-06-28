namespace MicrosoftTestingPlatformExamples.TestsWithMSTest;

[TestClass]
public sealed class Tests
{
    [TestMethod]
    public void FormatWelcomeMessage_ValidInput_ExpectedOutput()
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage("Hello");
        Assert.AreEqual("Hello Dev Leader!", result);
    }

    [DataTestMethod]
    [DataRow("Hello", "Hello Dev Leader!")]
    [DataRow("Welcome", "Welcome Dev Leader!")]
    [DataRow("Hi", "Hi Dev Leader!")]
    public void FormatWelcomeMessage_ValidInputs_ExpectedOutputs(
        string input, 
        string expected)
    {
        SomeDependency dependency = new();
        SystemUnderTest sut = new(dependency);

        var result = sut.FormatWelcomeMessage(input);
        Assert.AreEqual(expected, result);
    }
}