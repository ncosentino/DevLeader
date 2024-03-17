using Microsoft.AspNetCore.Http;

HttpContext ourContext = null; // init...
var baseUrl = ourContext.GetBaseUrl();
var url = HttpContextExtensionMethods.GetBaseUrl(ourContext);

public static class HttpContextExtensionMethods
{
    public static string GetBaseUrl(
        this HttpContext context)
    {
        var request = context.Request;
        var host = request.Host;
        var scheme = request.Scheme;
        var pathBase = request.PathBase;
        var url = $"{scheme}://{host}{pathBase}".TrimEnd('/');
        return url;
    }
}