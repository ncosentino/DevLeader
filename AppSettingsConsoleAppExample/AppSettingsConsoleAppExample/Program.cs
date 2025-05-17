using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");
var ourSetting = config["OurSetting"];

Console.WriteLine($"Connection string: {connectionString}");
Console.WriteLine($"Our setting: {ourSetting}");