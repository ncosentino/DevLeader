using System.Diagnostics;

using Microsoft.Diagnostics.Tracing.StackSources;
using static System.Runtime.InteropServices.JavaScript.JSType;

NumberGetter numberGetter = new();

Stopwatch sw = Stopwatch.StartNew();
numberGetter.GetNumbersLazy().Any();
Console.WriteLine($"Lazy: {sw.ElapsedMilliseconds}ms");
sw.Stop();

sw.Restart();
numberGetter.GetNumbersEager().Any();
sw.Stop();

Console.WriteLine($"Eager: {sw.ElapsedMilliseconds}ms");


class NumberGetter
{
    public IEnumerable<int> GetNumbersLazy()
    {
        Thread.Sleep(1000);
        yield return 1;
        Thread.Sleep(1000);
        yield return 2;
        Thread.Sleep(1000);
        yield return 3;
    }

    public IEnumerable<int> GetNumbersEager()
    {
        Thread.Sleep(3000);
        return new int[] { 1, 2, 3 };
    }
}
