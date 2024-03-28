using MongoDB.Bson;
using MongoDB.Driver;

using System.Text.Json;

/* This is top secret :) */
#region Connection String
const string ConnectionString = "mongodb+srv://USERNAME:PASSWORD@HOST";
#endregion

const string DatabaseName = "DevLeaderTest";

MongoClient client = new(ConnectionString);
var database = client.GetDatabase(DatabaseName);

var collection = database.GetCollection<BsonDocument>("YouTube");

var filterBuilder = Builders<BsonDocument>.Filter;
var filter = filterBuilder.Eq("Name", "Nick Cosentino");

var updateBuilder = Builders<BsonDocument>.Update;
var update = updateBuilder.Set("Name", "Dev Leader");

Console.WriteLine($"Updating records using UpdateOne...");
var result = collection.UpdateOne(
    filter,
    update);
Console.WriteLine(
    $"""
    Done updating records using UpdateOne.
      Matched: {result.MatchedCount}
      Modified: {result.ModifiedCount}
    """);

filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("_id", new ObjectId("6600ed7edb2d11dad50df975"));
update = updateBuilder.Set("Name", "Very Special");

Console.WriteLine($"Updating records using UpdateMany...");
var result2 = collection.UpdateMany(
    filter,
    update);
Console.WriteLine(
    $"""
    Done updating records using UpdateMany.
      Matched: {result2.MatchedCount}
      Modified: {result2.ModifiedCount}
    """);

Console.WriteLine($"Updating records using FindOneAndUpdate...");
filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("_id", new ObjectId("6600ed7edb2d11dad50df975"));
update = updateBuilder.Set("Name", "Dev Leader ROCKS");
var result3 = collection.FindOneAndUpdate(
    filter,
    update);
Console.WriteLine(
    $"""
    Done updating records using FindOneAndUpdate.
      Matched: {result3}
    """);

Console.WriteLine($"Replacing records using ReplaceOne...");
filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("_id", new ObjectId("6600ed7edb2d11dad50df975"));
BsonDocument someNewDocument = new()
{
    ["Some Field"] = "This schema is WAY different!",
};
var result4 = collection.ReplaceOne(
    filter,
    someNewDocument);
Console.WriteLine(
    $"""
    Done replacing records using ReplaceOne.
      Matched: {result4.MatchedCount}
      Modified: {result4.ModifiedCount}
    """);

Console.WriteLine($"Replacing records using FindOneAndReplace...");
filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("_id", new ObjectId("6600ed7edb2d11dad50df975"));
BsonDocument someNewDocument2 = new()
{
    ["Some Field"] = "This schema is WAY different!",
};
var result5 = collection.FindOneAndReplace(
    filter,
    someNewDocument2);
Console.WriteLine(
    $"""
    Done replacing records using FindOneAndReplace.
      Matched: {result5}
    """);