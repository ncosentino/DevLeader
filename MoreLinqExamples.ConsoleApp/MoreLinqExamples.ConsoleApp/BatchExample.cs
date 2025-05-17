// Watch videos on YouTube:
// - https://youtu.be/BVW1aDQU7mo
// - https://youtu.be/t-qWtP_TcTc
// - https://youtu.be/_MLajy6jw9o
using MoreLinq;

public sealed class BatchExample
{
    public void Run()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = numbers.Batch(3);
        //var result = numbers.Chunk(3);

        foreach (var batch in result)
        {
            foreach (var number in batch)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
        }
    }
}