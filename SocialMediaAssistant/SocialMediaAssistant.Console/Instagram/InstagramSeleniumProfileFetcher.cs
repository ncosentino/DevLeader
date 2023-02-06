// <strong title="Followers" data-e2e="followers-count">139</strong> -- tiktok html reference
// <span class=\"_ac2a _ac2b\" title=\".+?\"><span>(.+?)</span></span> -- instagram followers
// <span class="_ac2a _ac2b"><span>12</span></span> -- instagram following
// https://www.tiktok.com/@devleader

using System.Globalization;
using System.Text.RegularExpressions;

using OpenQA.Selenium;

using SocialMediaAssistant.Selenium;

public sealed class InstagramSeleniumProfileFetcher
{
    //Regex - Regular expression, define syntax that matches patterns in strings
    private static readonly Regex _followingCountRegex = new Regex(
        "<span class=\"_ac2a _ac2b\"><span>(.*?)</span></span> following",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex _followerCountRegex = new Regex(
        "<span class=\"_ac2a _ac2b\" title=\"(.*?)\"><span>(.*?)</span></span> followers",
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

        var followingCountResult = webDriver.WaitForPageSourceContains(
            "<div class=\"_aacl _aaco _aacu _aacy _aad6 _aadb _aade\">",
            TimeSpan.FromSeconds(15));

        // take text from page, go to location identified, collect X characters from there
        var followingCountMatch = _followingCountRegex.Match(
            followingCountResult.PageSource,
            followingCountResult.Index.Value,
            200);
        var followerCountMatch = _followerCountRegex.Match(
            followingCountResult.PageSource, 
            followingCountResult.Index.Value, 
            300);

        // type conversion (parse) from type String to type Integer; InvariantCulture builds resiliency
        var following = int.Parse(followingCountMatch.Groups[1].Value, CultureInfo.InvariantCulture);
        var followers = int.Parse(followerCountMatch.Groups[1].Value, CultureInfo.InvariantCulture);

        var profileInfo = new ProfileInfo(
            profileName,
            profileUrl,
            followers,
            following);

        // FIXME: convert this whole thing to be asynchronous at some point?
        return Task.FromResult(profileInfo);
    }
    
}




