using MoreLinq;

var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var result = numbers.Batch(3);

foreach (var batch in result)
{
    foreach (var number in batch)
    {
        Console.Write(number + " ");
    }

    Console.WriteLine();
}