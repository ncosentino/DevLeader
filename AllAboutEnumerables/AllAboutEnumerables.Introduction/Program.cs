// The code in this file is intended to accompany the video here:
// https://youtu.be/RR7Cq0iwNYo

// IEnumerables are a basic interface that allow us to iterate (i.e. ask for
// one element at a time) from a collection or data source. All collection
// types in C# inherit from this interface.

int[] myArray = new int[] { 1, 2, 3, 4, 5 };

// we can assign the array to an IEnumerable because it implements the interface
IEnumerable<int> myArrayAsEnumerable = myArray as IEnumerable<int>;

// we can use foreach
Console.WriteLine("Using foreach on the array...");
foreach (var item in myArray)
{
    Console.WriteLine(item);
}

Console.WriteLine("Using foreach on the enumerable...");
foreach (var item in myArrayAsEnumerable)
{
    Console.WriteLine(item);
}

// we cannot use for loops with indexing on IEnumerables! indexing is not
// supported by IEnumerable, but it does work for arrays
Console.WriteLine("Using for loop on the array...");
for (int i = 0; i < myArray.Length; i++)
{
    // works!
    Console.WriteLine(myArray[i]);
}

// we can't even get the length of the enumerable!
//for (int i = 0; i < myArrayAsEnumerable.Length; i++)
//{
//    // does not work!
//    Console.WriteLine(myArrayAsEnumerable[i]);
//}

//// this is similar for other collection types, like the List<T>
List<int> myList = new List<int> { 6, 7, 8, 9, 10 };

// we can assign the list to an IEnumerable because it implements the interface
IEnumerable<int> myListAsEnumerable = myList as IEnumerable<int>;

// we can use foreach
Console.WriteLine("Using foreach on the list...");
foreach (var item in myList)
{
    Console.WriteLine(item);
}

Console.WriteLine("Using foreach on the list enumerable...");
foreach (var item in myListAsEnumerable)
{
    Console.WriteLine(item);
}

//// let's have a quick look at what this means for return types!
IEnumerable<string> FunctionThatReturnsAnArrayAsEnumerable()
{
    return new string[] { "A", "B", "C" };
}

IEnumerable<string> FunctionThatReturnsAListAsEnumerable()
{
    return new List<string> { "A", "B", "C" };
}

// we can use foreach
Console.WriteLine("Using foreach on the array function...");
foreach (var item in FunctionThatReturnsAnArrayAsEnumerable())
{
    Console.WriteLine(item);
}

Console.WriteLine("Using foreach on the list function...");
foreach (var item in FunctionThatReturnsAListAsEnumerable())
{
    Console.WriteLine(item);
}

//// why would we want to do this? are there any implications?
//// :) let's find out in the next part!