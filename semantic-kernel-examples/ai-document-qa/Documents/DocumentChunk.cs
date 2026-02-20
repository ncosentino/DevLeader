using Microsoft.Extensions.VectorData;

namespace AiDocumentQA.Documents;

/// <summary>
/// A single chunk of a source document, stored in the vector store with its embedding.
/// </summary>
public sealed class DocumentChunk
{
    [VectorStoreKey]
    public string ChunkId { get; set; } = "";

    [VectorStoreData]
    public string DocumentName { get; set; } = "";

    [VectorStoreData]
    public string Content { get; set; } = "";

    // 1536 dimensions: compatible with text-embedding-ada-002 and text-embedding-3-small
    [VectorStoreVector(1536, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Embedding { get; set; }
}
