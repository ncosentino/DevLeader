//int YieldInt()
//{
//    yield return 123;
//}

IEnumerable<int> YieldInts()
{
    // open a connection
    Thread.Sleep(500);
    // execute the query
    Thread.Sleep(1000);
    // read the results and yield them back
    Thread.Sleep(10);

    return [123, 456];
}

var yieldIntsResult = YieldInts();
//Console.WriteLine("Enter to continue...");
//Console.ReadLine();

var materialized = yieldIntsResult;
foreach (var number in yieldIntsResult)
{
    Console.WriteLine($"Got number {number}.");
}

foreach (var number in materialized)
{
    Console.WriteLine($"Got number {number}.");
}

return;