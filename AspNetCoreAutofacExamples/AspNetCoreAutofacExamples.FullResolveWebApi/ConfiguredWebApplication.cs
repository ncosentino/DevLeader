using Plugin.SDK;

internal sealed class ConfiguredWebApplication(
    WebApplication _webApplication,
    IReadOnlyList<PreApplicationConfiguredMarker> _markers)
{
    public WebApplication WebApplication => _webApplication;
}