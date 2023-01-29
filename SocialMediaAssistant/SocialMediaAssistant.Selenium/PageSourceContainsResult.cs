// NOTE: dropping this into this namespace for ease of access
namespace OpenQA.Selenium
{
    public sealed record PageSourceContainsResult(
        string TextToFind,
        string? PageSource,
        int? Index)
    {
        public bool Success { get; } = Index != -1 && !string.IsNullOrWhiteSpace(PageSource);
    }
}
