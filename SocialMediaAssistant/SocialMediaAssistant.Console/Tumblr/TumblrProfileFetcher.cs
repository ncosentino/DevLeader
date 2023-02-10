// <strong title="Followers" data-e2e="followers-count">139</strong>
// https://www.tumblr.com/devleader

using DontPanic.TumblrSharp.Client;

using SocialMediaAssistant.Console;

namespace SocialMediaAssistant.Console.Tumblr
{
    public sealed class TumblrProfileFetcher : IProfileFetcher
    {
        private readonly TumblrClient _tumblrClient;

        public TumblrProfileFetcher(TumblrClient tumblrClient)
        {
            _tumblrClient = tumblrClient;
        }

        public async Task<ProfileInfo> FetchAsync(string profileName)
        {
            var blog = await _tumblrClient.GetBlogInfoAsync(profileName);
            var userInfo = await _tumblrClient.GetUserInfoAsync();
            var followersResult = await _tumblrClient.GetFollowersAsync(profileName, 0, 1);

            var profileInfo = new ProfileInfo(
                blog.Name,
                blog.Url,
                (int)followersResult.Count,
                (int)userInfo.FollowingCount);

            return profileInfo;
        }
    }
}