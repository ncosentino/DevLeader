// <strong title="Followers" data-e2e="followers-count">139</strong>
// https://www.tiktok.com/@devleader

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using System.Text.RegularExpressions;

ChromeOptions browser = new ChromeOptions();
browser.AddArgument("headless"); // come back to this

using IWebDriver webDriver = new ChromeDriver();

webDriver.Url = "https://www.tiktok.com/@devleader";

MatchResult followerCountResult = WaitForPageSourceContains(webDriver, "data-e2e=\"followers-count\"", TimeSpan.FromSeconds(15));

// Console.WriteLine(followerCount.ToString());

var regex = new Regex("data-e2e=\"followers-count\">(.+?)</strong>", RegexOptions.IgnoreCase | RegexOptions.Compiled); //Regex - Regular expression, define syntax that matches patterns in strings
var match = regex.Match(followerCountResult.PageSource, followerCountResult.Index.Value, 200); // take text from page, go to location identified, collect 200 characters from there
var followers = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture); // type conversion (parse) from type String to type Integer; InvariantCulture builds resiliency
Console.WriteLine($"Followers: {followers}"); // String interpolation

MatchResult WaitForPageSourceContains(
    IWebDriver driver,
    string textToFind,
    TimeSpan timeout)
{
    var wait = new WebDriverWait(driver, timeout);

    string? pageSource = null;
    var foundIndex = -1;
    var success = wait.Until(x => // anonymous delegate (google this later)
    {
    pageSource = x.PageSource;
    foundIndex = pageSource.IndexOf(textToFind, StringComparison.Ordinal);
    return foundIndex != -1; // -1 means it was not found, so keep waiting
    });

    return new MatchResult(
        foundIndex == -1 ? null : pageSource,
        foundIndex);
    }

    public sealed record MatchResult(
        string? PageSource,
        int? Index)
    {
        public bool Success { get; } = Index != -1 && !string.IsNullOrWhiteSpace(PageSource);
    }


