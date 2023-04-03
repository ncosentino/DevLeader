using System.Text;
using System.Text.RegularExpressions;

AwesomeUrlSaver urlSaver = new(
    new UrlNormalizer(),
    new HtmlContentDownloader(),
    new HtmlUrlExtractor(),
    new UrlListOutputFormatter(),
    new UrlContentFileWriter());
await urlSaver.SaveUrlsAsync(
    "https://www.devleader.ca",
    "urls.txt");

public sealed class AwesomeUrlSaver
{
    private readonly UrlNormalizer _urlNormalizer;
    private readonly HtmlContentDownloader _htmlContentDownloader;
    private readonly HtmlUrlExtractor _htmlUrlExtractor;
    private readonly UrlListOutputFormatter _urlListOutputFormatter;
    private readonly UrlContentFileWriter _urlContentFileWriter;

    public AwesomeUrlSaver(
        UrlNormalizer urlNormalizer,
        HtmlContentDownloader htmlContentDownloader,
        HtmlUrlExtractor htmlUrlExtractor,
        UrlListOutputFormatter urlListOutputFormatter,
        UrlContentFileWriter urlContentFileWriter)
    {
        _urlNormalizer = urlNormalizer;
        _htmlContentDownloader = htmlContentDownloader;
        _htmlUrlExtractor = htmlUrlExtractor;
        _urlListOutputFormatter = urlListOutputFormatter;
        _urlContentFileWriter = urlContentFileWriter;
    }

    public async Task SaveUrlsAsync(
        string websiteUrl,
        string outputFilePath)
    {
        // NOTE: please keep in mind this is a
        // contrived system that's been put
        // together to demonstrate several
        // concepts, so please do not focus
        // very specifically on the choices for
        // logic & functionality that are being
        // used in this class!

        // Functionality: input validation & sanity checking!
        ArgumentException.ThrowIfNullOrEmpty(websiteUrl);
        ArgumentException.ThrowIfNullOrEmpty(outputFilePath);

        // Functionality: We only deal with https
        // urls and we need to ensure they are valid!
        var normalizedUrl = _urlNormalizer
            .NormalizeUrl(websiteUrl);

        // Functionality: Fetch the content from
        // the interwebzzz
        var htmlContent = await _htmlContentDownloader
            .DownloadHtmlContentAsync(normalizedUrl);

        // Functionality: Extract the content
        // from the html
        var urls = _htmlUrlExtractor
            .BuildUrlListFromHtml(htmlContent);

        // Functionality: Format the output
        var outputBuilder = _urlListOutputFormatter
            .FormatUrlListOutput(normalizedUrl, urls);

        // Functionality: Output the text!
        _urlContentFileWriter
            .WriteUrlContentToFile(outputFilePath, outputBuilder.ToString());
    }
}

public sealed class UrlContentFileWriter
{
    public void WriteUrlContentToFile(
        string outputFilePath,
        string content)
    {
        var fullOutputFilePath = Path.GetFullPath(outputFilePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullOutputFilePath));
        File.WriteAllText(
            fullOutputFilePath,
            content);
    }
}

public sealed class UrlListOutputFormatter
{
    public StringBuilder FormatUrlListOutput(
      string normalizedUrl,
      List<string> urls)
    {
        StringBuilder outputBuilder = new();
        outputBuilder.AppendLine($"All of the URLs from '{normalizedUrl}':");

        foreach (string url in urls)
        {
            outputBuilder.AppendLine(url);
        }

        return outputBuilder;
    }
}

public sealed class HtmlUrlExtractor
{
    public List<string> BuildUrlListFromHtml(string htmlContent)
    {
        Regex hrefRegex = new(
            @"href\s*=\s*'([0-9a-z:/\.\-]+)'", // FIXME: is this regex sufficient?!
            RegexOptions.IgnoreCase);

        List<string> urls = new();
        foreach (Match match in hrefRegex.Matches(htmlContent))
        {
            urls.Add(match.Groups[1].Value);
        }

        return urls;
    }
}

public sealed class HtmlContentDownloader
{
    public async Task<string> DownloadHtmlContentAsync(string url)
    {
        using HttpClient client = new();
        var htmlContent = await client.GetStringAsync(url);
        return htmlContent;
    }
}

public sealed class UrlNormalizer
{
    public string NormalizeUrl(string websiteUrl)
    {
        string normalizedUrl;
        if (websiteUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
        {
            normalizedUrl = $"https://{websiteUrl[7..]}";
        }
        else if (!websiteUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            normalizedUrl = $"https://{websiteUrl}";
        }
        else
        {
            normalizedUrl = websiteUrl;
        }

        if (!Uri.TryCreate(normalizedUrl, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException(
                $"Could not make '{websiteUrl}' into a valid https URI.");
        }

        return normalizedUrl;
    }
}