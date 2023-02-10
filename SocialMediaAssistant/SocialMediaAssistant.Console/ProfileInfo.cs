namespace SocialMediaAssistant.Console
{
    public sealed record ProfileInfo(
    string ProfileName,
    string ProfileUrl,
    int NumberOfFollowers,
    int NumberOfFollowing);
}