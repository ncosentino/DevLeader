using System.Text;
using System.Text.RegularExpressions;

AwesomeUrlSaver urlSaver = new();
await urlSaver.SaveUrlsAsync(
    "https://www.devleader.ca",
    "urls.txt");

public sealed class AwesomeUrlSaver
{
    public async Task SaveUrlsAsync(
        string websiteUrl,
        string outputFilePath)
    {
        // NOTE: please keep in mind this is a contrived system that's been
        // put together to demonstrate several concepts, so please do not
        // focus very specifically on the choices for logic & functionality
        // that are being used in this class!

        // Functionality: input validation & sanity checking!
        ArgumentException.ThrowIfNullOrEmpty(websiteUrl);
        ArgumentException.ThrowIfNullOrEmpty(outputFilePath);

        // Functionality: We only deal with https urls and we need to ensure they are valid!
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

        // Functionality: Fetch the content from the interwebzzz
        using HttpClient client = new();
        var htmlContent = await client.GetStringAsync(normalizedUrl);

        // Functionality: Extract the content from the html
        Regex hrefRegex = new(
            @"href\s*=\s*'([0-9a-z:/\.\-]+)'", // FIXME: is this regex sufficient?!
            RegexOptions.IgnoreCase);

        List<string> urls = new();
        foreach (Match match in hrefRegex.Matches(htmlContent))
        {
            urls.Add(match.Groups[1].Value);
        }

        // Functionality: Format the output
        StringBuilder outputBuilder = new();
        outputBuilder.AppendLine($"All of the URLs from '{normalizedUrl}':");

        foreach (string url in urls)
        {
            outputBuilder.AppendLine(url);
        }

        // Functionality: Output the text!
        var fullOutputFilePath = Path.GetFullPath(outputFilePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullOutputFilePath));
        File.WriteAllText(
            fullOutputFilePath,
            outputBuilder.ToString());
    }
}