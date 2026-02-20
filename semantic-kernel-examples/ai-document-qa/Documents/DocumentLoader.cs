namespace AiDocumentQA.Documents;

/// <summary>
/// Loads text files from a directory and splits them into paragraph-based chunks.
/// </summary>
public static class DocumentLoader
{
    private const int MaxWordsPerChunk = 400;

    public static IEnumerable<DocumentChunk> LoadFromDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"Documents directory not found: {directoryPath}");

        var files = Directory.GetFiles(directoryPath, "*.txt")
            .Concat(Directory.GetFiles(directoryPath, "*.md"))
            .OrderBy(f => f);

        foreach (var filePath in files)
        {
            var documentName = Path.GetFileName(filePath);
            var text = File.ReadAllText(filePath);
            var chunks = SplitIntoChunks(text, documentName);

            foreach (var chunk in chunks)
                yield return chunk;
        }
    }

    private static IEnumerable<DocumentChunk> SplitIntoChunks(string text, string documentName)
    {
        // Split on blank lines to get paragraphs, then group into word-count-bounded chunks
        var paragraphs = text
            .Split(["\r\n\r\n", "\n\n"], StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim())
            .Where(p => p.Length > 0);

        var currentChunk = new List<string>();
        int currentWordCount = 0;
        int chunkIndex = 0;

        foreach (var paragraph in paragraphs)
        {
            int paragraphWords = CountWords(paragraph);

            // If adding this paragraph would exceed the limit, flush current chunk first
            if (currentWordCount + paragraphWords > MaxWordsPerChunk && currentChunk.Count > 0)
            {
                yield return CreateChunk(documentName, chunkIndex++, currentChunk);
                currentChunk.Clear();
                currentWordCount = 0;
            }

            currentChunk.Add(paragraph);
            currentWordCount += paragraphWords;
        }

        // Flush any remaining content
        if (currentChunk.Count > 0)
            yield return CreateChunk(documentName, chunkIndex, currentChunk);
    }

    private static DocumentChunk CreateChunk(string documentName, int index, List<string> paragraphs) =>
        new()
        {
            ChunkId = $"{documentName}::chunk-{index:D4}",
            DocumentName = documentName,
            Content = string.Join("\n\n", paragraphs)
        };

    private static int CountWords(string text) =>
        text.Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Length;
}
