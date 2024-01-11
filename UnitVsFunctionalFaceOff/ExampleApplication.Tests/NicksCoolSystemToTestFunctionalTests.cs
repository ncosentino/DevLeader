using Xunit;

namespace ExampleApplication.Tests;

public class NicksCoolSystemToTestFunctionalTests
{
    [Fact]
    private async Task GetTitlesAsync_ReturnsExpectedTitles()
    {
        // Arrange
        DummyHttpClientFactory httpClientFactory = new();
        DevLeaderFeedFetcher devLeaderFeedFetcher = new(
            httpClientFactory);
        HtmlElementFinder htmlElementFinder = new();
        var systemToTest = new NicksCoolSystemToTest(
            devLeaderFeedFetcher,
            htmlElementFinder);

        // Act
        var titles = await systemToTest.GetTitlesAsync();

        // Assert
        Assert.Equal(12, titles.Count);
        Assert.Equal("Dev Leader", titles[0]);
        Assert.Equal("Dev Leader", titles[1]);
        Assert.Equal(
            "What Does Refactoring Code Mean? " +
            "What New Developers Need To Know", 
            titles[2]);
        //Assert.Equal("Dev Leader", titles[3]);
        // ...
        //Assert.Equal("Dev Leader", titles[10]);
        Assert.Equal(
            "Exploring Examples Of The " +
            "Mediator Pattern In C#", 
            titles[11]);
    }
}
