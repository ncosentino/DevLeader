using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () =>
{
    var contents = "<xml><message>Hello, World!</message></xml>";
    var stream = new MemoryStream(Encoding.UTF8.GetBytes(contents));
    return Results.File(stream, "application/xml");
});
app.MapGet("/serialize", () =>
{
    var record = new XmlRecord()
    {
        Message = "Hello, World!"
    };
    return Results.Extensions.Xml(record);
});

app.Run();

[Route("controller")]
public sealed class XmlController : ControllerBase
{
    [HttpGet]
    public IActionResult GetXml()
    {
        var contents = "<xml><message>Hello, World!</message></xml>";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(contents));
        return File(stream, "application/xml");
    }
}



public sealed class XmlRecord
{
    public string Message { get; set; }
}

public sealed class XmlResult<T> : IResult
{
    private static readonly XmlSerializer Serializer = new(typeof(T));

    private readonly T result;

    public XmlResult(T result)
    {
        this.result = result;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        using var stream = new MemoryStream();
        Serializer.Serialize(httpContext.Response.Body, this.result);

        httpContext.Response.ContentType = "application/xml";
        stream.Position = 0;
        await stream.CopyToAsync(httpContext.Response.Body);
    }
}

internal static class XmlResultExtensions
{
    public static IResult Xml<T>(this IResultExtensions _, T result)
        => new XmlResult<T>(result);
}