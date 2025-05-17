using MoreLinq;

var source1 = Enumerable
    .Range(1, 5)
    .Select(x => x.ToString())
    .ToArray();
var source2 = Enumerable
    .Range(1, 10)
    .Select(x => x.ToString())
    .Reverse()
    .ToArray();

var zip = source1
    .Zip(source2, (x, y) => (x, y))
    .ToArray();
Console.WriteLine("Zip:");
foreach (var item in zip)
{
    Console.WriteLine(item);
}

var zipLongest = source1
    .ZipLongest(source2, (x, y) => (x, y))
    .ToArray();
Console.WriteLine("ZipLongest:");
foreach (var item in zipLongest)
{
    Console.WriteLine(item);
}

var zipShortest = source1
    .ZipShortest(source2, (x, y) => (x, y))
    .ToArray();
Console.WriteLine("ZipShortest:");
foreach (var item in zipShortest)
{
    Console.WriteLine(item);
}

var manualZipShortest = source1
    .ManualZipShortest(source2, (x, y) => (x, y))
    .ToArray();
Console.WriteLine("ManualZipShortest:");
foreach (var item in manualZipShortest)
{
    Console.WriteLine(item);
}

public static class ManualExtensions
{
    public static IEnumerable<TResult> ManualZipShortest<T1, T2, TResult>(
        this IEnumerable<T1> source1,
        IEnumerable<T2> source2,
        Func<T1, T2, TResult> selector)
    {
        using var enumerator1 = source1.GetEnumerator();
        using var enumerator2 = source2.GetEnumerator();
        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            yield return selector.Invoke(
                enumerator1.Current,
                enumerator2.Current);
        }
    }
}