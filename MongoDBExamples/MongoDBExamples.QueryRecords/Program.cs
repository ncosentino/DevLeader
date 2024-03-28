using MongoDB.Bson;
using MongoDB.Driver;

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

// Example 1: AND
filter = filterBuilder.And(
    filter,
    filterBuilder.Eq("Name", "Nick Cosentino"));
//filter &= filterBuilder.Eq("Name", "Nick Cosentino");

// Example 2: OR
filter = filterBuilder.Empty;
filter |= filterBuilder.Eq("Name", "Doesn't Match Anything");

// Example 3: AND and OR
filter = filterBuilder.Empty;
filter &= filterBuilder.Eq("Name", "No match here");
filter |= filterBuilder.Eq("Subscribers", 1000000);

Console.WriteLine("Finding records...");
var results = collection.Find(filter);

Console.WriteLine("Converting results to list...");
var resultsList = results.ToList();
Console.WriteLine($"{resultsList.Count} results in the list.");

Console.WriteLine("Enumerating results...");
foreach (var result in results.ToEnumerable())
{
    Console.WriteLine($"\t{result}");
}

Console.WriteLine("Done enumerating results.");

Console.WriteLine("Finding records and converting to cursor...");
using var resultsCursor = collection
    .Find(
        filter,
        new FindOptions()
        {
            BatchSize = 2,
        })
    .ToCursor();

Console.WriteLine("MoveNext() loop for cursor...");
while (resultsCursor.MoveNext())
{
    foreach (var result in resultsCursor.Current)
    {
        Console.WriteLine($"\t{result}");
    }
}
Console.WriteLine("Done MoveNext() loop for cursor.");
