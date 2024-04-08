using MongoDB.Bson;
using MongoDB.Driver;

using Testcontainers.MongoDb;

var container = new MongoDbBuilder()
    .WithImage("mongo:latest")
    //.WithPortBinding(27017, 27017)
    .Build();
await container.StartAsync();

var mongoClient = new MongoClient(container.GetConnectionString());
var database = mongoClient.GetDatabase("test_db");
var collection = database.GetCollection<BsonDocument>("test_collection");

await collection.InsertOneAsync(new BsonDocument()
{
    ["Name"] = "Nick Cosentino",
});

var doc = await collection
    .Find(Builders<BsonDocument>.Filter.Empty)
    .FirstOrDefaultAsync();
Console.WriteLine($"Found document: {doc}");

await container.DisposeAsync();

Console.WriteLine("Press enter to exit...");
Console.ReadLine();