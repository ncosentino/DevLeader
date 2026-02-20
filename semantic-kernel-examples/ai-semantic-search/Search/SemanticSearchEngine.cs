using AiSemanticSearch.Models;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Embeddings;

namespace AiSemanticSearch.Search;

/// <summary>
/// Result returned from a semantic search query.
/// </summary>
public sealed record SearchResult(SearchItem Item, double Score);

/// <summary>
/// Performs semantic search over an indexed corpus using vector similarity.
/// Supports optional category filtering.
/// </summary>
public sealed class SemanticSearchEngine
{
    private readonly ITextEmbeddingGenerationService _embeddingService;
    private readonly VectorStoreCollection<string, SearchItem> _collection;

    public SemanticSearchEngine(
        ITextEmbeddingGenerationService embeddingService,
        VectorStoreCollection<string, SearchItem> collection)
    {
        _embeddingService = embeddingService;
        _collection = collection;
    }

    /// <summary>
    /// Embeds the query and returns the top-k most similar items, optionally filtered by category.
    /// </summary>
    public async Task<IReadOnlyList<SearchResult>> SearchAsync(
        string query,
        int topK = 5,
        string? categoryFilter = null,
        CancellationToken cancellationToken = default)
    {
        var queryEmbeddings = await _embeddingService
            .GenerateEmbeddingsAsync([query], cancellationToken: cancellationToken);

        var queryVector = queryEmbeddings[0];

        // Build optional filter expression for the category field (v10: lambda expression)
        VectorSearchOptions<SearchItem>? options = null;
        if (!string.IsNullOrWhiteSpace(categoryFilter))
        {
            var cat = categoryFilter; // capture for lambda
            options = new VectorSearchOptions<SearchItem>
            {
                Filter = item => item.Category == cat
            };
        }

        var searchable = (IVectorSearchable<SearchItem>)_collection;
        var results = searchable.SearchAsync(queryVector, topK, options, cancellationToken);

        var output = new List<SearchResult>();
        await foreach (var result in results.WithCancellation(cancellationToken))
            output.Add(new SearchResult(result.Record, result.Score ?? 0.0));

        return output;
    }
}
