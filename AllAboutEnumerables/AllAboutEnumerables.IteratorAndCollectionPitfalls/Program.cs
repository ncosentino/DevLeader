// The code in this file is intended to accompany the video here:
// https://youtu.be/3a_hg8KE-aQ

// From my professional experience, I have seen BOTH IEnumerables/Iterators as
// well as using full collections without iterators cause nightmarish problems
// in REAL production code that's *actually* being sold for money with REAL
// customers that use it. So when I explain this, please understand that it is
// not just some theoretical thing... this is based on real world experiences.

// To start, why would anyone ever care about using an iterator? Why don't we
// just return Lists and Collections all the time so we can avoid any weird
// stuff that happens with enumerables?

// Iterators afford you the luxury of being able to exit early from iteration
// because you are streaming your data in. When you use full collections, you
// do *not* get the flexibility to be able to do this. That might be totally
// fine for your architectural design, but it's absolutely a trade off.

// these examples will be a little bit contrived, but they are to demonstrate
// the trade-offs being made... there are ways to work around this without
// having to use iterators, but I am showing you examples of things I have seen.
using System.Diagnostics;
using System.Diagnostics.Tracing;

List<string> PretendThisGoesToADatabaseAsList()
{
    // let's simulate some exaggerated latency to the DB
    Thread.Sleep(5000);
    Console.WriteLine($"{DateTime.Now} - <DB now sending back results>");

    // now let's assume we run some query that pulls back 100,000 strings from
    // the database
    List<string> results = new List<string>();
    while (results.Count < 100_000)
    {
        // simulate a tiny bit of latency on the "reader" that would be
        // reading data back from the database... every so often we'll
        // sleep a little bit just to slow it down
        if ((results.Count % 100) == 0)
        {
            Thread.Sleep(1);
        }

        results.Add(Guid.NewGuid().ToString());
    }

    return results;
}

long memoryBefore = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Getting data from the database using List...");
List<string> databaseResultsList = PretendThisGoesToADatabaseAsList();
Console.WriteLine($"{DateTime.Now} - Got data from the database using List.");

Console.WriteLine($"{DateTime.Now} - Has Data: {databaseResultsList.Any()}");
Console.WriteLine($"{DateTime.Now} - Count of Data: {databaseResultsList.Count}");

long memoryAfter = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Memory Increase (bytes): {memoryAfter - memoryBefore}");

// okay, so what would an iterator do for us? well... let's try it!
IEnumerable<string> PretendThisGoesToADatabaseAsIterator()
{
    // let's simulate some exaggerated latency to the DB
    Thread.Sleep(5000);
    Console.WriteLine($"{DateTime.Now} - <DB now sending back results>");

    // now let's assume we run some query that pulls back 100,000 strings from
    // the database
    for (int i = 0; i < 100_000; i++)
    {
        // simulate a tiny bit of latency on the "reader" that would be
        // reading data back from the database... every so often we'll
        // sleep a little bit just to slow it down
        if ((i % 100) == 0)
        {
            Thread.Sleep(1);
        }

        yield return Guid.NewGuid().ToString();
    }
}

memoryBefore = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Getting data from the database using iterator...");
IEnumerable<string> databaseResultsIterator = PretendThisGoesToADatabaseAsIterator();
Console.WriteLine($"{DateTime.Now} - \"Got data\" (not actually... it's lazy evaluated) from the database using iterator.");

Console.WriteLine($"{DateTime.Now} - Has Data: {databaseResultsIterator.Any()}");
Console.WriteLine($"{DateTime.Now} - Finished checking if database has data using iterator.");
Console.WriteLine($"{DateTime.Now} - Count of Data: {databaseResultsIterator.Count()}");
Console.WriteLine($"{DateTime.Now} - Finished counting data from database using iterator.");

memoryAfter = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Memory Increase (bytes): {memoryAfter - memoryBefore}");

// Wow!!! Look! So much less memory used! But wait... why did we pay a perf
// hit for Any() and Count()? Why did it say we hit the database twice?!

// This stems from the fact that an iterator is more like a function pointer
// than it is a collection. Calling Any() and Count() actually force
// reiterating over the data because we are not iterating over a "materialized"
// collection. It's important to note that calling Any() did *NOT* pay the full
// performance hit... just the initial database latency time because it was able
// to exit early.

// So... how do we fix this?

// In this case, because this example is contrived, we'd just materialize what
// we need from our result a single time. Instead of iterating twice, we could
// materialize once and use that data

memoryBefore = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Getting data from the database using iterator so we can materialize it once...");
List<string> materializedFromIterator = PretendThisGoesToADatabaseAsIterator().ToList();
Console.WriteLine($"{DateTime.Now} - Materialized data.");

// NOTE: we are using the materialized list here
Console.WriteLine($"{DateTime.Now} - Has Data: {materializedFromIterator.Any()}");
Console.WriteLine($"{DateTime.Now} - Finished checking if database has data using materialized data.");
Console.WriteLine($"{DateTime.Now} - Count of Data: {materializedFromIterator.Count()}");
Console.WriteLine($"{DateTime.Now} - Finished counting data from database using materialized data.");

memoryAfter = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Memory Increase (bytes): {memoryAfter - memoryBefore}");

// WAIT!! the memory usage is back up super high!

// That's right. We essentially used our iterator and put ourselves RIGHT back
// to the spot in the beginning like the non-iterator.

// Let's be smarter on this last attempt. We don't need to store the whole data set.
memoryBefore = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Getting data from the database using iterator so we can materialize it once...");
int materializedCount = PretendThisGoesToADatabaseAsIterator().Count();
Console.WriteLine($"{DateTime.Now} - Materialized data.");

// NOTE: we are using the materialized list here
Console.WriteLine($"{DateTime.Now} - Has Data: {materializedCount > 0}");
Console.WriteLine($"{DateTime.Now} - Finished checking if database has data using materialized data.");
Console.WriteLine($"{DateTime.Now} - Count of Data: {materializedCount}");
Console.WriteLine($"{DateTime.Now} - Finished counting data from database using materialized data.");

memoryAfter = GC.GetTotalMemory(true);
Console.WriteLine($"{DateTime.Now} - Memory Increase (bytes): {memoryAfter - memoryBefore}");

// So in this case, iterators afforded us the flexibility to make a decision
// about how we want to call our function and what behavior results.
// - Yes, you could 100% go write better individual database queries to answer
//   what these examples are demonstrating. No, in real life people often miss
//   this and cause performance hits without realizing it because they don't
//   understand the tradeoffs properly
// - Iterators gave us flexibility, but they also allowed us to write some
//   dangerous code that caused multiple calls out to the database because
//   we tried to re-iterate. Yes, this happens ALL of the time in real code.

// Next up... let's dive deeper into measuring performance