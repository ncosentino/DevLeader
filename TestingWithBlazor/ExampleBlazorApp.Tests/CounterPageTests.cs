using Bunit;

using ExampleBlazorApp.Pages;

using Microsoft.AspNetCore.Components;

using Xunit;

namespace ExampleBlazorApp.Tests;

public class CounterPageTests : TestContext
{
    [Fact]
    public void Header_DefaultState_ExpectedTitle()
    {
        var renderedComponent = RenderComponent<Counter>();            
        renderedComponent
            .Find("h1")
            .MarkupMatches("<h1>Counter</h1>");
    }

    [Fact]
    public void CounterLabel_DefaultState_ExpectedValue()
    {
        var renderedComponent = RenderComponent<Counter>();
        renderedComponent
            .Find("p")
            .MarkupMatches("<p role=\"status\">Current count: 0</p>");
    }

    [Fact]
    public void CounterLabel_CountIsSet_ExpectedValue()
    {
        var renderedComponent = RenderComponent<Counter>();
        
        renderedComponent.SetParametersAndRender(
            parameters => parameters.Add<int>(p => p.currentCount, 5));

        renderedComponent
            .Find("p")
            .MarkupMatches("<p role=\"status\">Current count: 5</p>");
    }

    [Fact]
    public void PageAttribute_SingleAsExpected()
    {
        var pageAttributes = typeof(Counter).GetCustomAttributes(true);
        var pageRouteAttribute = (RouteAttribute)Assert.Single(
            pageAttributes,
            x => x is RouteAttribute routeAttribute);
        Assert.Equal("/counter", pageRouteAttribute.Template);
    }
}