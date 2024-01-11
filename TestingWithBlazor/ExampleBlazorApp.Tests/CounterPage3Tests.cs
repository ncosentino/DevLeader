using Bunit;

using ExampleBlazorApp.Pages;

using Xunit;

namespace ExampleBlazorApp.Tests;

public class CounterPage3Tests : TestContext
{
    [Fact]
    public void Header_DefaultState_ExpectedTitle()
    {
        var renderedComponent = RenderComponent<Counter3>();            
        renderedComponent
            .Find("h1")
            .MarkupMatches("<h1>Counter 3</h1>");
    }

    [Fact]
    public void CounterLabel_DefaultState_ExpectedValue()
    {
        var renderedComponent = RenderComponent<Counter3>();
        renderedComponent
            .Find("p")
            .MarkupMatches("<p role=\"status\">Current count: 0</p>");
    }

    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [Theory]
    public void CounterLabel_SimulatedIncrementClicks_ExpectedValue(
        int numberOfClicks)
    {
        var renderedComponent = RenderComponent<Counter3>();

        var buttonElement = renderedComponent.Find("button");
        for (int i = 0; i < numberOfClicks; i++)
        {
            buttonElement.Click();
        }

        renderedComponent
            .Find("p")
            .MarkupMatches($"<p role=\"status\">Current count: {numberOfClicks}</p>");
    }
}