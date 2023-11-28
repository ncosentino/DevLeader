HttpClientWrapper httpClient = new(new HttpClient());
BlogFetcher blogFetcher = new(httpClient);
LegacySystem legacySystem = new(blogFetcher);
await legacySystem.DoSomethingAsync();

//
// PLEASE NOTE: I do not want you to read this code thinking that it should be
// reproduced. There are patterns/practices here that I would NOT encourage
// you or anyone else to follow. This is to demonstrate a legacy code system
// and the ONLY focus that I would like you to have is that we can come here
// make changes, and make it feel better than when we arrived.
//
public sealed class LegacySystem
{
    private readonly BlogFetcher _blogFetcher;

    public LegacySystem(BlogFetcher blogFetcher)
    {
        _blogFetcher = blogFetcher;
    }

    public async Task DoSomethingAsync()
    {
        if (!File.Exists("blog.html") || 
            DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
        {
            await _blogFetcher.GetSomeBlogArticleAsync();
        }

        var theBlogHtml = File.ReadAllText("blog.html");
        var processedHtml = await ProcessHtmlAsync(theBlogHtml);
        await File.WriteAllTextAsync(
            $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.html",
            processedHtml);
    }

    private Task<string> ProcessHtmlAsync(string inputHtml)
    {
        // this is a silly example... but let's roll with it
        var processed = inputHtml.ToLower();
        return Task.FromResult(processed);
    }

    // OLD
    //private static void GetSomeBlogArticle()
    //{
    //    var someHtml = new HttpClient()
    //        .GetStringAsync("https://www.devleader.ca/2023/08/17/building-projects-unlock-success-as-a-beginner-programmer/")
    //        .GetAwaiter()
    //        .GetResult();

    //    // look there's a bug here!
    //    someHtml = someHtml.Replace(
    //        "Dev Leader", 
    //        "Some Total Nerd", StringComparison.OrdinalIgnoreCase);

    //    File.WriteAllText("blog.html", someHtml);
    //}
}

public sealed class BlogFetcher
{
    private readonly IHttpClient _httpClient;

    public BlogFetcher(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetSomeBlogArticleAsync()
    {
        // let's clean up:
        // - if we can: not touch file system
        var someHtml = await _httpClient.GetStringAsync(
            "https://www.devleader.ca/" +
            "2023/08/17/" +
            "building-projects-unlock-success-as-a-beginner-programmer/");

        await File.WriteAllTextAsync("blog.html", someHtml);
    }
}

public interface IHttpClient
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

    public async Task<string> GetStringAsync(string url)
    {
        return await _httpClient.GetStringAsync(url);
    }
}