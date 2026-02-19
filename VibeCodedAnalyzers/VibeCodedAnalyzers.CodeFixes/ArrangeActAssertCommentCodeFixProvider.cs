using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;

using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using VibeCodedAnalyzers.Analyzers;

namespace VibeCodedAnalyzers.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ArrangeActAssertCommentCodeFixProvider)), Shared]
public sealed class ArrangeActAssertCommentCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(ArrangeActAssertCommentAnalyzer.DiagnosticId);

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root == null)
        {
            return;
        }

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Remove Arrange/Act/Assert comment",
                createChangedDocument: c => RemoveCommentAsync(context.Document, root, diagnosticSpan, c),
                equivalenceKey: nameof(ArrangeActAssertCommentCodeFixProvider)),
            diagnostic);
    }

    private static Task<Document> RemoveCommentAsync(
        Document document,
        SyntaxNode root,
        Microsoft.CodeAnalysis.Text.TextSpan diagnosticSpan,
        CancellationToken cancellationToken)
    {
        // Find the trivia at the diagnostic location
        var triviaAtSpan = root.FindTrivia(diagnosticSpan.Start);

        if (triviaAtSpan.Kind() == SyntaxKind.None)
        {
            return Task.FromResult(document);
        }

        var token = triviaAtSpan.Token;
        var triviaList = token.LeadingTrivia.Contains(triviaAtSpan)
            ? token.LeadingTrivia
            : token.TrailingTrivia;

        var isLeading = token.LeadingTrivia.Contains(triviaAtSpan);

        // Build new trivia list excluding the AAA comment and associated whitespace/newlines
        var newTriviaList = RemoveCommentAndAssociatedTrivia(triviaList, triviaAtSpan);

        var newToken = isLeading
            ? token.WithLeadingTrivia(newTriviaList)
            : token.WithTrailingTrivia(newTriviaList);

        var newRoot = root.ReplaceToken(token, newToken);

        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }

    private static SyntaxTriviaList RemoveCommentAndAssociatedTrivia(
        SyntaxTriviaList triviaList,
        SyntaxTrivia commentToRemove)
    {
        var triviaArray = triviaList.ToList();
        var index = triviaArray.IndexOf(commentToRemove);

        if (index < 0)
        {
            return triviaList;
        }

        // We need to remove:
        // 1. The whitespace immediately before the comment (same line indentation)
        // 2. The comment itself
        // 3. The end-of-line immediately after the comment
        //
        // The trivia structure for a comment line like "        // Arrange\n" followed by
        // "        var sut = ..." is:
        // [EndOfLine from prev line] [Whitespace] [Comment] [EndOfLine] [Whitespace for next statement]
        //
        // We want to remove [Whitespace] [Comment] [EndOfLine] but keep the whitespace for the next statement.

        // First, check if there's whitespace immediately before the comment
        // that is on the same line (i.e., preceded by EndOfLine or at start)
        bool removeLeadingWhitespace = false;
        if (index > 0 && triviaArray[index - 1].IsKind(SyntaxKind.WhitespaceTrivia))
        {
            // Check if this whitespace is the start of the comment's line
            // It should be preceded by an EndOfLine or be at the start of the trivia list
            if (index == 1 || triviaArray[index - 2].IsKind(SyntaxKind.EndOfLineTrivia))
            {
                removeLeadingWhitespace = true;
            }
        }

        // Remove the comment itself
        triviaArray.RemoveAt(index);

        // If there's an end-of-line trivia immediately after where the comment was, remove it too
        if (index < triviaArray.Count && triviaArray[index].IsKind(SyntaxKind.EndOfLineTrivia))
        {
            triviaArray.RemoveAt(index);
        }

        // Now remove the leading whitespace if applicable (index has shifted, so it's now at index - 1)
        if (removeLeadingWhitespace)
        {
            triviaArray.RemoveAt(index - 1);
        }

        return SyntaxFactory.TriviaList(triviaArray);
    }
}
