// 1. install docker desktop
// 2. get redis image (or run directly!)
// 3. use the StackExchange.Redis NuGet package

using StackExchange.Redis;

using ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost:11337");
IDatabase db = multiplexer.GetDatabase();

//db.StringSet("mykey", "Dev Leader was here!");
string? value = db.StringGet("mykey");

Console.WriteLine(value);
