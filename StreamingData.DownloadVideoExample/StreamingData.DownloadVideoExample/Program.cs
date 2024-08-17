using System.Diagnostics;

HttpClient client = new HttpClient();

var url = "https://download.samplelib.com/mp4/sample-30s.mp4";

// NOTE: Yes, I know this is not a "benchmark" and stopwatch
// can be a poor choice for measuring performance. This is just
// a simple example to show the difference between the two options.
// Please take a deep breath :)
Stopwatch stopwatch = new();

stopwatch.Start();
var response1 = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
var stream1 = await response1.Content.ReadAsStreamAsync();
stopwatch.Stop();
Console.WriteLine(
    $"""
    ResponseContentRead:
    Duration: {stopwatch.ElapsedMilliseconds} ms
    Length: {stream1.Length} bytes
    Can Seek: {stream1.CanSeek}
    
    """);


stopwatch.Restart();
var response2 = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
var stream2 = await response2.Content.ReadAsStreamAsync();
stopwatch.Stop();

var hasLengthHeader = response2.Headers.TryGetValues(
    "Content-Length",
    out var contentLengthHeaders);
var rawLength = hasLengthHeader 
    ? contentLengthHeaders.First()
    : "Unknown";
var length = long.TryParse(rawLength, out var lengthValue)
    ? lengthValue
    : 0;

Console.WriteLine(
    $"""
    ResponseHeadersRead:
    Duration: {stopwatch.ElapsedMilliseconds} ms
    Length: {length} bytes
    Can Seek: {stream2.CanSeek}

    """);

class StreamWithLength : Stream
{
    private readonly Stream _inner;

    public StreamWithLength(
        Stream inner,
        long length)
    {
        _inner = inner;
        Length = length;
    }

    public override bool CanRead => throw new NotImplementedException();

    public override bool CanSeek => _inner.CanSeek;

    public override bool CanWrite => _inner.CanWrite;

    public override long Length { get; }

    public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Flush()
    {
        throw new NotImplementedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }
}