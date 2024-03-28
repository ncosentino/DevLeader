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
var filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("Name", "Nick Cosentino");

Console.WriteLine("Deleting records using DeleteOne...");
var result = collection.DeleteOne(filter);
Console.WriteLine($"Deleted {result.DeletedCount} records.");

Console.WriteLine("Deleting records using DeleteMany...");
var result2 = collection.DeleteMany(filter);
Console.WriteLine($"Deleted {result2.DeletedCount} records.");

Console.WriteLine("Deleting records using FindOneAndDelete...");
var result3 = collection.FindOneAndDelete(filter);
Console.WriteLine($"Deleted record: {result3}");