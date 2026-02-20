using AiSemanticSearch.Models;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Embeddings;

namespace AiSemanticSearch.Indexing;

/// <summary>
/// Embeds corpus items in batch and stores them in the vector store.
/// </summary>
public sealed class CorpusIndexer
{
    private const string CollectionName = "corpus";

    private readonly ITextEmbeddingGenerationService _embeddingService;
    private readonly VectorStoreCollection<string, SearchItem> _collection;

    public CorpusIndexer(
        ITextEmbeddingGenerationService embeddingService,
        VectorStore vectorStore)
    {
        _embeddingService = embeddingService;
        _collection = vectorStore.GetCollection<string, SearchItem>(CollectionName);
    }

    /// <summary>
    /// Embeds all items in the corpus and upserts them into the vector store.
    /// </summary>
    public async Task IndexAsync(
        IEnumerable<SearchItem> items,
        CancellationToken cancellationToken = default)
    {
        await _collection.EnsureCollectionExistsAsync(cancellationToken);

        var itemList = items.ToList();
        Console.WriteLine($"  Embedding {itemList.Count} items...");

        // Embed the combined title + body for richer semantic matching
        var texts = itemList.Select(item => $"{item.Title}\n{item.Body}").ToList();
        var embeddings = await _embeddingService.GenerateEmbeddingsAsync(texts, cancellationToken: cancellationToken);

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].Embedding = embeddings[i];
            await _collection.UpsertAsync(itemList[i], cancellationToken: cancellationToken);
        }

        Console.WriteLine($"  Indexed {itemList.Count} items.");
    }

    /// <summary>Exposes the collection for search.</summary>
    public VectorStoreCollection<string, SearchItem> Collection => _collection;
}
