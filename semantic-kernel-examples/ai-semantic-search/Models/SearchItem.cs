using Microsoft.Extensions.VectorData;

namespace AiSemanticSearch.Models;

/// <summary>
/// A single searchable item in the corpus, stored with its embedding.
/// </summary>
public sealed class SearchItem
{
    [VectorStoreKey]
    public string Id { get; set; } = "";

    [VectorStoreData]
    public string Title { get; set; } = "";

    [VectorStoreData]
    public string Body { get; set; } = "";

    /// <summary>
    /// Optional category for filtered search (e.g., "concept", "howto", "troubleshooting").
    /// </summary>
    [VectorStoreData]
    public string Category { get; set; } = "";

    // 1536 dimensions: compatible with text-embedding-ada-002 and text-embedding-3-small
    [VectorStoreVector(1536, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Embedding { get; set; }
}
