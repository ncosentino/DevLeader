DummyHttpClientFactory httpClientFactory = new();
DevLeaderFeedFetcher devLeaderFeedFetcher = new(httpClientFactory);
HtmlElementFinder htmlElementFinder = new();
NicksCoolSystemToTest nicksCoolSystem = new(
    devLeaderFeedFetcher,
    htmlElementFinder);
var titles = await nicksCoolSystem.GetTitlesAsync();
foreach (var title in titles)
{
    Console.WriteLine(title);
}