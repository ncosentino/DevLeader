using System.Diagnostics;
using System.Net.Http;

Console.WriteLine("Enter the endpoint:");
var endpoint = Console.ReadLine();

Console.WriteLine("Enter the number of requests:");
var requestCount = int.Parse(Console.ReadLine());

/*
//Console.WriteLine("Prime cache with single request? y/n");
//var primeCache = string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase);
//if (primeCache)
//{
//    Console.WriteLine("Priming...");
//    var client = HttpClientFactory.Create();
//    await client.GetAsync(endpoint);
//    Console.WriteLine("Primed.");
//}
*/

Console.WriteLine("Starting...");
var tasks = Enumerable.Range(0, requestCount).Select(async _ =>
{
    var client = HttpClientFactory.Create();

    var watch = Stopwatch.StartNew();
    await client.GetAsync(endpoint);
    watch.Stop();

    //Console.WriteLine(watch.ElapsedMilliseconds);
}).ToArray();
await Task.WhenAll(tasks);

Console.WriteLine("Done");
Console.WriteLine("Press enter to exit.");
Console.ReadLine();