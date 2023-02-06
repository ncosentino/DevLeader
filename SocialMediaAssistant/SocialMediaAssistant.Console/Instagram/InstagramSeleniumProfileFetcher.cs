// <strong title="Followers" data-e2e="followers-count">139</strong> -- tiktok html reference
// <span class=\"_ac2a _ac2b\" title=\".+?\"><span>(.+?)</span></span> -- instagram followers
// <span class="_ac2a _ac2b"><span>12</span></span> -- instagram following
// https://www.instagram.com/dev.leader

using System.Globalization;
using System.Text.RegularExpressions;

using OpenQA.Selenium;

using SocialMediaAssistant.Selenium;

public sealed class InstagramSeleniumProfileFetcher
{
    //Regex - Regular expression, define syntax that matches patterns in strings
    private static readonly Regex _metaContextRegex = new Regex(
        "(\\d+) Followers, (\\d+) Following, (\\d+) Posts",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly ChromeWebdriverFactory _chromeWebdriverFactory;

    public InstagramSeleniumProfileFetcher()
    {
        // FIXME: we'll look at Autofac for this later
        _chromeWebdriverFactory = new ChromeWebdriverFactory();
    }

    public Task<ProfileInfo> FetchAsync(string profileName)
    {
        if (profileName.StartsWith("@"))
        {
            profileName = profileName.Substring(1);
        }

        var profileUrl = $"https://www.instagram.com/{profileName}";

        using IWebDriver webDriver = _chromeWebdriverFactory.Create();
        webDriver.Url = profileUrl;

        var metaDataResult = webDriver.WaitForPageSourceContains(
            $"href=\"https://www.instagram.com/{profileName}/\"><meta content=\"",
            TimeSpan.FromSeconds(30));

        // take text from page, go to location identified, collect X characters from there
        var metaContentMatch = _metaContextRegex.Match(
            metaDataResult.PageSource,
            metaDataResult.Index.Value,
            200);

        // type conversion (parse) from type String to type Integer; InvariantCulture builds resiliency
        var followers = int.Parse(metaContentMatch.Groups[1].Value, CultureInfo.InvariantCulture);
        var following = int.Parse(metaContentMatch.Groups[2].Value, CultureInfo.InvariantCulture);
        //var posts = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
            
        var profileInfo = new ProfileInfo(
            profileName,
            profileUrl,
            followers,
            following);

        // FIXME: convert this whole thing to be asynchronous at some point?
        return Task.FromResult(profileInfo);
    }
    
}




