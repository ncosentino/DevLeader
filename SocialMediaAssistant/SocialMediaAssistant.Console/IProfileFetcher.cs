namespace SocialMediaAssistant.Console
{
    public interface IProfileFetcher
    {
        Task<ProfileInfo> FetchAsync(string profileName);
    }
}