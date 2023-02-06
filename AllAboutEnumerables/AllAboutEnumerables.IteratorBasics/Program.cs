// The code in this file is intended to accompany the video here:
// https://youtu.be/fU5-QeSLG2I
// You can also follow along with the blog post here:
// https://www.devleader.ca/2023/02/10/iterators-an-elementary-perspective-on-how-they-function/

int[] dataset = new int[] { 1, 2, 3, 4, 5 };

// function that returns the collection directly
IEnumerable<int> SimpleEnumerable()
{
    return dataset;
}

// function that is an "iterator"
IEnumerable<int> SimpleIterator()
{
    foreach (var item in dataset)
    {
        // check out this fancy keyword "yield"!
        yield return item;
    }
}

Console.WriteLine("Foreach in simple iterator...");
foreach (var item in SimpleIterator())
{
    Console.WriteLine(item);
}

// okay so what? who cares about the yield return?

// consider that we could perform some logic at different staps inside of the
// iteration process within the function
IEnumerable<int> ConsoleWritingIterator()
{
    Console.WriteLine("Printing to console at start of iterator!");
    foreach (var item in dataset)
    {
        Console.WriteLine("Printing to console before yield return!");
        yield return item;
        Console.WriteLine("Printing to console after yield return!");
    }

    Console.WriteLine("Printing to console at end of iterator!");
}


Console.WriteLine("Foreach in console writing iterator...");
foreach (var item in ConsoleWritingIterator())
{
    Console.WriteLine(item);
}

// some other neat properties? iterators are... lazy?!
IEnumerable<int> FunctionThatSleepsFor5SecondsFirst()
{
    Thread.Sleep(5000);
    return dataset;
}

IEnumerable<int> IteratorThatSleepsFor5SecondsFirst()
{
    Thread.Sleep(5000);
    foreach (var item in dataset)
    {
        yield return item;
    }
}

// this will wait the full 5 seconds because it is *not* an iterator, it is
// simply a function that has an IEnumerable return type.
Console.WriteLine($"Calling function that sleeps for 5 seconds first: {DateTime.Now}");
var resultA = FunctionThatSleepsFor5SecondsFirst();
Console.WriteLine($"Finished function that sleeps for 5 seconds first: {DateTime.Now}");

// this assignment will happen "instantly" because it's an actual iterator (
// uses yield return)... but... why?!
Console.WriteLine($"Calling iterator that sleeps for 5 seconds first: {DateTime.Now}");
var resultB = IteratorThatSleepsFor5SecondsFirst();
Console.WriteLine($"Finished iterator that sleeps for 5 seconds first: {DateTime.Now}");

// wait... Why did that happen instantly?!
// ...
// ...
