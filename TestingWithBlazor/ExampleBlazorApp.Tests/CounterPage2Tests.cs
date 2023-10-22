using Bunit;

using ExampleBlazorApp.Pages;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace ExampleBlazorApp.Tests;

public class CounterPage2Tests : IDisposable
{
    private readonly MockRepository _mockRepository;
    private readonly TestContext _testContext;
    private readonly Mock<ICounterViewModel> _counterViewModel;

    public CounterPage2Tests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _counterViewModel = _mockRepository.Create<ICounterViewModel>();

        _testContext = new TestContext();
        _testContext
            .Services
            .AddSingleton(_counterViewModel.Object);
    }

    public void Dispose()
    {
        _testContext.Dispose();
    }

    [Fact]
    public void Header_DefaultState_ExpectedTitle()
    {
        _counterViewModel
            .Setup(x => x.CurrentCount)
            .Returns(0);

        var renderedComponent = _testContext.RenderComponent<Counter2>();
        renderedComponent
            .Find("h1")
            .MarkupMatches("<h1>Counter 2</h1>");

        _mockRepository.VerifyAll();
    }

    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [Theory]
    public void CounterLabel_CountIsSet_ExpectedValue(
        int counterValue)
    {
        _counterViewModel
            .Setup(x => x.CurrentCount)
            .Returns(counterValue);

        var renderedComponent = _testContext.RenderComponent<Counter2>();
        
        var expectedMarkup = 
            $"<p role=\"status\">" +
            $"Current count: {counterValue}" +
            $"</p>";
        renderedComponent
            .Find("p")
            .MarkupMatches(expectedMarkup);

        _mockRepository.VerifyAll();
    }
}