using AiDocumentQA.Documents;
using AiDocumentQA.Retrieval;
using Microsoft.SemanticKernel;

namespace AiDocumentQA.QA;

/// <summary>
/// Retrieves relevant document chunks and uses them to answer questions
/// via a Semantic Kernel prompt function (RAG pattern).
/// </summary>
public sealed class QuestionAnswerer
{
    private readonly DocumentIndexer _indexer;
    private readonly Kernel _kernel;
    private readonly KernelFunction _answerFn;

    public QuestionAnswerer(DocumentIndexer indexer, Kernel kernel)
    {
        _indexer = indexer;
        _kernel = kernel;

        // Inline prompt function: augments the question with retrieved context
        _answerFn = KernelFunctionFactory.CreateFromPrompt(
            """
            You are a helpful assistant answering questions based only on the provided context.
            If the context does not contain enough information to answer, say so clearly.
            Do not use any knowledge outside the provided context.

            Context:
            {{$context}}

            Question: {{$question}}

            Answer:
            """,
            functionName: "AnswerFromContext",
            description: "Answers a question using only the provided document context");
    }

    /// <summary>
    /// Retrieves the top-k relevant chunks for the question and generates a grounded answer.
    /// </summary>
    public async Task<string> AnswerAsync(
        string question,
        int topK = 3,
        CancellationToken cancellationToken = default)
    {
        // Step 1: Retrieve relevant chunks via vector similarity search
        var relevantChunks = await _indexer.SearchAsync(question, topK, cancellationToken);

        if (relevantChunks.Count == 0)
            return "No relevant context found in the indexed documents.";

        // Step 2: Build context string from retrieved chunks
        var context = BuildContext(relevantChunks);

        // Step 3: Invoke the QA prompt with the augmented context
        var args = new KernelArguments
        {
            ["context"] = context,
            ["question"] = question
        };

        var result = await _kernel.InvokeAsync(_answerFn, args, cancellationToken);
        return result.GetValue<string>() ?? "Unable to generate an answer.";
    }

    private static string BuildContext(IReadOnlyList<DocumentChunk> chunks)
    {
        var parts = chunks.Select((chunk, i) =>
            $"[Source {i + 1}: {chunk.DocumentName}]\n{chunk.Content}");

        return string.Join("\n\n---\n\n", parts);
    }
}
