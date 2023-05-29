/* Examples borrowed & interpreted from this page:
 * https://code-maze.com/csharp-span-to-improve-application-performance/
 * 
 * Notes on spans:
 * - allocation-free representation of contiguous memory 
 *   (as in, does not copy)
 * - is a "ref struct", so cannot be used in async 
 *   methods, or as a field in a class
 * - Spans are allocated on the stack
 * - cannot be used in lambdas
 * - can use Memory<T> and ReadOnlyMemory<T> to work 
 *   around this and get span-like goodness
 */

using System.Text;

using BenchmarkDotNet.Attributes;

namespace SpanExamles.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class EmptyLinesBenchmarks
    {
        private string _textToSearch = 
            """
            this is

            just

            some sample text

            with

            new lines

            but this line and the next
            do not have any new lines
            between them

            but the line above is empty
            """;

        [Params(1_000, 10_000, 100_000)]
        public int TextLength;

        [GlobalSetup]
        public void Setup()
        {
            Random random = new(1337);
            var buffer = new byte[TextLength];
            random.NextBytes(buffer);

            int index = 0;
            while (index < buffer.Length - 1 - Environment.NewLine.Length)
            {
                if (random.NextDouble() <= 0.10)
                {
                    for (int i = 0; i < Environment.NewLine.Length; i++)
                    {
                        buffer[index++] = (byte)Environment.NewLine[i];
                    }

                    // this makes the actual empty line though!
                    if (random.NextDouble() <= 0.50)
                    {
                        for (int i = 0; i < Environment.NewLine.Length; i++)
                        {
                            buffer[index++] = (byte)Environment.NewLine[i];
                        }
                    }

                    continue;
                }

                index++;
            }

            _textToSearch = Encoding.ASCII.GetString(buffer);
        }

        [Benchmark]
        public void Substrings()
        {
            var currentIndex = 0;
            var counter = 0;

            char lastTargetChar = Environment.NewLine[1];
            var lastTargetCharIndex = 0;

            foreach (char c in _textToSearch)
            {
                if (c == lastTargetChar)
                {
                    currentIndex += 1;
                    var line = _textToSearch.Substring(
                        lastTargetCharIndex, 
                        currentIndex - lastTargetCharIndex);
                    if (line.Equals(
                        Environment.NewLine,
                        StringComparison.Ordinal))
                    {
                        counter++;
                    }

                    lastTargetCharIndex = currentIndex;
                    continue;
                }

                currentIndex++;
            }
        }

        [Benchmark]
        public void ParseWithSpan()
        {
            var spanOfTextToSearch = _textToSearch.AsSpan();
            
            var currentIndex = 0;
            var counter = 0;

            char lastTargetChar = Environment.NewLine[1];
            var lastTargetCharIndex = 0;

            foreach (char c in spanOfTextToSearch)
            {
                if (c == lastTargetChar)
                {
                    currentIndex += 1;
                    var slice = spanOfTextToSearch.Slice(
                        lastTargetCharIndex, 
                        currentIndex - lastTargetCharIndex);

                    if (slice.Equals(
                        Environment.NewLine,
                        StringComparison.Ordinal))
                    {
                        counter++;
                    }

                    lastTargetCharIndex = currentIndex;
                    continue;
                }

                currentIndex++;
            }
        }
    }
}
