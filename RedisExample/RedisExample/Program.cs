// 1. install docker desktop
// 2. get redis image (or run directly!)
// 3. use the StackExchange.Redis NuGet package

using StackExchange.Redis;

using ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost");
IDatabase db = multiplexer.GetDatabase();

//db.StringSet("mykey", "Dev Leader was here!");
//string? value = db.StringGet("mykey");

while (true)
{
    Console.WriteLine("Enter a key to get a value, or type 'exit' to quit:");
    string? key = Console.ReadLine();
    if (key == "exit")
    {
        break;
    }

    var value = await db.StringGetAsync(key);
    Console.WriteLine($"Retrieved {key}: {value}");
}
