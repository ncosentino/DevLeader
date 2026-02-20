using AiDocumentQA.Documents;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Embeddings;

namespace AiDocumentQA.Retrieval;

/// <summary>
/// Embeds document chunks and stores them in the vector store, then retrieves
/// relevant chunks for a query using vector similarity search.
/// </summary>
public sealed class DocumentIndexer
{
    private const string CollectionName = "documents";

    private readonly ITextEmbeddingGenerationService _embeddingService;
    private readonly VectorStoreCollection<string, DocumentChunk> _collection;

    public DocumentIndexer(
        ITextEmbeddingGenerationService embeddingService,
        VectorStore vectorStore)
    {
        _embeddingService = embeddingService;
        _collection = vectorStore.GetCollection<string, DocumentChunk>(CollectionName);
    }

    /// <summary>
    /// Embeds all chunks from the document loader and upserts them into the vector store.
    /// </summary>
    public async Task IndexDocumentsAsync(
        IEnumerable<DocumentChunk> chunks,
        CancellationToken cancellationToken = default)
    {
        await _collection.EnsureCollectionExistsAsync(cancellationToken);

        var chunkList = chunks.ToList();
        Console.WriteLine($"  Embedding {chunkList.Count} chunks...");

        // Generate embeddings in batch for efficiency
        var contents = chunkList.Select(c => c.Content).ToList();
        var embeddings = await _embeddingService.GenerateEmbeddingsAsync(contents, cancellationToken: cancellationToken);

        for (int i = 0; i < chunkList.Count; i++)
        {
            chunkList[i].Embedding = embeddings[i];
            await _collection.UpsertAsync(chunkList[i], cancellationToken: cancellationToken);
        }

        Console.WriteLine($"  Indexed {chunkList.Count} chunks from {chunkList.Select(c => c.DocumentName).Distinct().Count()} document(s).");
    }

    /// <summary>
    /// Embeds the query and retrieves the top-k most similar chunks via vector search.
    /// </summary>
    public async Task<IReadOnlyList<DocumentChunk>> SearchAsync(
        string query,
        int topK = 3,
        CancellationToken cancellationToken = default)
    {
        var queryEmbeddings = await _embeddingService.GenerateEmbeddingsAsync(
            [query], cancellationToken: cancellationToken);

        var queryVector = queryEmbeddings[0];

        // IVectorSearchable<T>.SearchAsync<TVector>(vector, top, options?, ct) -- top is positional in v10
        var searchable = (IVectorSearchable<DocumentChunk>)_collection;

        var results = searchable.SearchAsync(queryVector, topK, cancellationToken: cancellationToken);

        var chunks = new List<DocumentChunk>();
        await foreach (var result in results.WithCancellation(cancellationToken))
            chunks.Add(result.Record);

        return chunks;
    }
}
