public interface IHttpClientFactory
{
    IHttpClient CreateClient(string name);
}

public sealed class DummyHttpClientFactory : IHttpClientFactory
{
    public IHttpClient CreateClient(string name)
    {
        HttpClient httpClient = new();
        HttpClientWrapper wrapper = new(httpClient);
        return wrapper;
    }
}

public interface IHttpClient : IDisposable
{
    Task<string> GetStringAsync(string url);
}

public sealed class HttpClientWrapper : IHttpClient
{
    private readonly HttpClient _httpClient;

    public HttpClientWrapper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<string> GetStringAsync(string url)
    {
        return _httpClient.GetStringAsync(url);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}

public sealed class NicksCoolSystemToTest
{
    private readonly DevLeaderFeedFetcher _devLeaderFeedFetcher;
    private readonly HtmlElementFinder _htmlElementFinder;

    public NicksCoolSystemToTest(
        DevLeaderFeedFetcher devLeaderFeedFetcher,
        HtmlElementFinder htmlElementFinder)
    {
        _devLeaderFeedFetcher = devLeaderFeedFetcher;
        _htmlElementFinder = htmlElementFinder;
    }

    public async Task<IReadOnlyList<string>> GetTitlesAsync()
    {
        var feedContent = await _devLeaderFeedFetcher
            .FetchFeedAsync();
        var titles = _htmlElementFinder.GetElementValues(
            feedContent,
            "title");
        return titles;
    }
}

public sealed class DevLeaderFeedFetcher
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DevLeaderFeedFetcher(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> FetchFeedAsync()
    {
        using var httpClient = _httpClientFactory
            .CreateClient("devleader");
        var feedContent = await httpClient
            .GetStringAsync("https://devleader.ca/feed/");
        return feedContent;
    }
}

public sealed class HtmlElementFinder
{
    public IReadOnlyList<string> GetElementValues(
        string content,
        string markerName)
    {
        List<string> elementValues = new();
        string markerStart = $"<{markerName}>";
        string markerEnd = $"</{markerName}>";

        int startIndex = 0;
        while (true)
        {
            var markerStartIndex = content.IndexOf(
                markerStart,
                startIndex,
                StringComparison.OrdinalIgnoreCase);
            if (markerStartIndex < 0)
            {
                break;
            }

            var markerEndIndex = content.IndexOf(
                markerEnd,
                markerStartIndex,
                StringComparison.OrdinalIgnoreCase);
            if (markerEndIndex < 0)
            {
                break;
            }

            var title = content.Substring(
                markerStartIndex + markerStart.Length,
                markerEndIndex - markerStartIndex - markerStart.Length);

            // FIXME: We want to exclude all "Dev Leader" to get article titles.
            // This would be the fix for this behavior.
            if (!"Dev Leader".Equals(title, StringComparison.Ordinal))
            {
                elementValues.Add(title);
            }

            elementValues.Add(title);

            startIndex = markerEndIndex + markerEnd.Length;
        }

        return elementValues;
    }
}