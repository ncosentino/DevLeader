// LINQ stands for Language Integrated Query
// We get access to a bunch of LINQ methods in the System.Linq namespace
// that operate on IEnumerable<T>

// LINQ can help us
// - map
// - filter
// - reduce

//// map: transform each element in a collection
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

//List<string> numbersAsStrings = new();
//foreach (int number in numbers)
//{
//    numbersAsStrings.Add(number.ToString());
//}

//// using LINQ we could do...
//var numbersAsStrings2 = numbers
//    .Select(number => number.ToString())
//    .ToList();


//// filter: remove elements from a collection
//List<int> evenNumbers = new();
//foreach (int number in numbers)
//{
//    if (number % 2 == 0)
//    {
//        evenNumbers.Add(number);
//    }
//}

//// using LINQ we could do...
//var evenNumbers2 = evenNumbers
//    .Where(number => number % 2 == 0)
//    .ToList();

//// reduce: combine elements in a collection
//int sum = 0;
//foreach (int number in numbers)
//{
//    sum += number;
//}

//// using LINQ we could do...
//var sum2 = numbers.Sum();


//// LINQ methods are lazy... but what's that even mean?!
//// It means that when you call a LINQ method on an IEnumerable<T>
//// it doesn't actually do anything:
//// ***
//// until you iterate over the result
//// ***
//var lazyNumbersAsStrings = numbers
//    .Select(number =>
//    {
//        Console.WriteLine($"Transforming {number} to a string");
//        return number.ToString();
//    });
//Console.WriteLine("After the LINQ line for lazyNumbersAsStrings");

//// force enumeration
//lazyNumbersAsStrings.ToArray();
//Console.WriteLine("After forcing enumeration lazyNumbersAsStrings");


//// this also means you need to be careful if your LINQ is expensive
//// and you keep re-evaluating it because you didn't store the result
//// in a variable!
//Console.ReadLine();
//var expensiveToCalculate = numbers
//    .Select(number =>
//    {
//        Console.WriteLine($"Transforming {number} to a string");
//        Thread.Sleep(1000);
//        return number.ToString();
//    })
//    .ToArray();

//Console.WriteLine("Before first enumeration of expensive operation...");
//foreach (var numberAsString in expensiveToCalculate)
//{
//    Console.WriteLine(numberAsString);
//}

//Console.WriteLine("Before second enumeration of expensive operation...");
//foreach (var numberAsString in expensiveToCalculate)
//{
//    Console.WriteLine(numberAsString);
//}


// we can make our own LINQ methods!
var myLinqResult = numbers
    .NicksFancyLinqMethod(number => number * 2)
    .ToArray();

foreach (var number in myLinqResult)
{
    Console.WriteLine(number);
}

public static class MyLinq
{
    public static IEnumerable<T> NicksFancyLinqMethod<T>(
        this IEnumerable<T> source,
        Func<T, T> selector)
    {
        foreach (T item in source)
        {
            Console.WriteLine($"Applying selector to {item}");
            yield return selector(item);
        }
    }
}