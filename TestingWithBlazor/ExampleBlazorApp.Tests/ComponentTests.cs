using Bunit;

using ExampleBlazorApp.Components;

using Xunit;

namespace ExampleBlazorApp.Tests;

public class ComponentTests : TestContext
{
    [Fact]
    public void Header_DefaultState_ExpectedTitle()
    {
        var renderedComponent = RenderComponent<Component>();
        
        renderedComponent
            .Find("h3")
            .MarkupMatches("<h3>Component</h3>");
        Assert.Empty(renderedComponent.FindAll("h1"));
    }
}