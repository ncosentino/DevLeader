var tikTokProfileFetcher = new TikTokSeleniumProfileFetcher();
var profileInfo = await tikTokProfileFetcher.FetchAsync("devleader");

Console.WriteLine(profileInfo);