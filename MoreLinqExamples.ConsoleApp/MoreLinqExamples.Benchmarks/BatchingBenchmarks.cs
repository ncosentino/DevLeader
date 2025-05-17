// Watch videos on YouTube:
// - https://youtu.be/BVW1aDQU7mo
// - https://youtu.be/t-qWtP_TcTc
// - https://youtu.be/_MLajy6jw9o

using BenchmarkDotNet.Attributes;

using MoreLinq;

[MemoryDiagnoser]
[ShortRunJob]
public class BatchingBenchmarks
{
    private int[]? _numbers;

    [Params(10, 1_000, 1_000_000, 10_000_000)]
    public int CollectionSize;

    [Params(1, 1_000, 100_000, 1_000_000)]
    public int BatchSize;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _numbers = Enumerable.Range(1, CollectionSize).ToArray();
    }

    //[Benchmark]
    //public void ManualBatchingMaterialized()
    //{
    //    foreach (var batch in ManualBatchingMaterialized(
    //        _numbers!,
    //        BatchSize))
    //    {
    //        foreach (var number in batch)
    //        {
    //            // Do nothing
    //        }
    //    }
    //}

    //[Benchmark]
    //public void ManualBatchingStreaming()
    //{
    //    foreach (var batch in ManualBatchingStreaming(
    //        _numbers!,
    //        BatchSize))
    //    {
    //        foreach (var number in batch)
    //        {
    //            // Do nothing
    //        }
    //    }
    //}

    [Benchmark]
    public void MoreLinqBatching()
    {
        foreach (var batch in _numbers!.Batch(BatchSize))
        {
            foreach (var number in batch)
            {
                // Do nothing
            }
        }
    }

    [Benchmark(Baseline = true)]
    public void LinqChunk()
    {
        foreach (var batch in _numbers!.Chunk(BatchSize))
        {
            foreach (var number in batch)
            {
                // Do nothing
            }
        }
    }

    private IEnumerable<IReadOnlyList<int>> ManualBatchingStreaming(
        int[] numbers, 
        int batchSize)
    {
        var batch = new List<int>(batchSize);

        foreach (var number in numbers)
        {
            batch.Add(number);

            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<int>(batchSize);
            }
        }

        if (batch.Count > 0)
        {
            yield return batch;
        }
    }

    private IReadOnlyList<IReadOnlyList<int>> ManualBatchingMaterialized(
        int[] numbers,
        int batchSize)
    {
        var batches = new List<IReadOnlyList<int>>();

        var batch = new List<int>(batchSize);

        foreach (var number in numbers)
        {
            batch.Add(number);

            if (batch.Count == batchSize)
            {
                batches.Add(batch);
                batch = new List<int>(batchSize);
            }
        }

        if (batch.Count > 0)
        {
            batches.Add(batch);
        }

        return batches;
    }
}