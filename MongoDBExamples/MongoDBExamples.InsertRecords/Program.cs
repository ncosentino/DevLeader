//
// Example of using MongdoDB in C# for inserting records.
// More info can be found here:
// - https://youtu.be/0fB9qg-oR04
// - https://www.devleader.ca/2024/03/22/mongodb-in-c-simplified-guide-for-inserting-data/
//
using MongoDB.Bson;
using MongoDB.Driver;

/* This is top secret :) */
#region Connection String
const string ConnectionString = "mongodb+srv://USERNAME:PASSWORD@HOST";
#endregion

const string DatabaseName = "DevLeaderTest";

MongoClient client = new(ConnectionString);
var database = client.GetDatabase(DatabaseName);

Console.WriteLine("Writing records using BsonDocument...");
var collection = database.GetCollection<BsonDocument>("YouTube");
collection.InsertOne(new BsonDocument()
{
    { "Name", "Nick Cosentino" },
    { "Channel", "DevLeader" },
    { "Subscribers", 1_000_000 }
});
await collection.InsertOneAsync(new BsonDocument()
{
    { "Name", "Nick Cosentino" },
    { "Channel", "DevLeader" },
    { "Subscribers", 1_000_000 }
});

Console.WriteLine("Writing records using record DTO...");
var collection2 = database.GetCollection<YouTubeInfo>("YouTube");
collection2.InsertOne(new YouTubeInfo(
    "Nick Cosentino",
    "DevLeader",
    1_000_000));

// async version...

record YouTubeInfo(
    string Name,
    string Channel, 
    int Subscribers);