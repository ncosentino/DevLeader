using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using VibeCodedAnalyzers.Analyzers;

namespace VibeCodedAnalyzers.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AssertMessageCodeFixProvider)), Shared]
public sealed class AssertMessageCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(
            AssertMessageAnalyzer.DiagnosticIdTrue,
            AssertMessageAnalyzer.DiagnosticIdFalse);

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

        var invocation = root.FindNode(diagnosticSpan)
            .AncestorsAndSelf()
            .OfType<InvocationExpressionSyntax>()
            .FirstOrDefault();

        if (invocation == null)
        {
            return;
        }

        var isAssertTrue = diagnostic.Id == AssertMessageAnalyzer.DiagnosticIdTrue;
        var assertMethodName = isAssertTrue ? "Assert.True" : "Assert.False";

        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Add message parameter to {assertMethodName}",
                createChangedDocument: c => AddMessageParameterAsync(
                    context.Document,
                    root,
                    invocation,
                    isAssertTrue,
                    c),
                equivalenceKey: nameof(AssertMessageCodeFixProvider) + diagnostic.Id),
            diagnostic);
    }

    private static Task<Document> AddMessageParameterAsync(
        Document document,
        SyntaxNode root,
        InvocationExpressionSyntax invocation,
        bool isAssertTrue,
        CancellationToken cancellationToken)
    {
        var argumentList = invocation.ArgumentList;
        if (argumentList == null || argumentList.Arguments.Count == 0)
        {
            return Task.FromResult(document);
        }

        var firstArgument = argumentList.Arguments[0];
        var firstArgumentExpression = firstArgument.Expression;

        var messageExpression = GenerateMessageExpression(firstArgumentExpression, isAssertTrue);

        var newArgument = SyntaxFactory.Argument(messageExpression);
        var newArgumentList = argumentList.AddArguments(newArgument);

        var newInvocation = invocation.WithArgumentList(newArgumentList);
        var newRoot = root.ReplaceNode(invocation, newInvocation);

        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }

    private static ExpressionSyntax GenerateMessageExpression(
        ExpressionSyntax firstArgumentExpression,
        bool isAssertTrue)
    {
        var expectedValue = isAssertTrue ? "true" : "false";

        // Case 1: Variable (IdentifierNameSyntax)
        if (firstArgumentExpression is IdentifierNameSyntax identifier)
        {
            return CreateNameofMessageExpression(identifier.Identifier.Text, expectedValue);
        }

        // Case 2: Method invocation (InvocationExpressionSyntax)
        if (firstArgumentExpression is InvocationExpressionSyntax methodInvocation)
        {
            var methodName = GetMethodName(methodInvocation);
            if (methodName != null)
            {
                return CreateNameofMessageExpression(methodName, expectedValue);
            }
        }

        // Case 3: Member access that could be a property or field
        if (firstArgumentExpression is MemberAccessExpressionSyntax memberAccess)
        {
            var memberName = memberAccess.Name.Identifier.Text;
            return CreateNameofMessageExpression(memberName, expectedValue);
        }

        // Case 4: Expression (anything else - binary expressions, etc.)
        return CreateExpressionMessageExpression(expectedValue);
    }

    private static string GetMethodName(InvocationExpressionSyntax invocation)
    {
        if (invocation.Expression is IdentifierNameSyntax simpleIdentifier)
        {
            return simpleIdentifier.Identifier.Text;
        }

        if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
        {
            return memberAccess.Name.Identifier.Text;
        }

        return null;
    }

    private static InterpolatedStringExpressionSyntax CreateNameofMessageExpression(
        string identifierName,
        string expectedValue)
    {
        // Create: $"Expected to get {expectedValue} for '{nameof(identifierName)}'."
        return SyntaxFactory.InterpolatedStringExpression(
            SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken),
            SyntaxFactory.List(new InterpolatedStringContentSyntax[]
            {
                SyntaxFactory.InterpolatedStringText()
                    .WithTextToken(SyntaxFactory.Token(
                        SyntaxFactory.TriviaList(),
                        SyntaxKind.InterpolatedStringTextToken,
                        $"Expected to get {expectedValue} for '",
                        $"Expected to get {expectedValue} for '",
                        SyntaxFactory.TriviaList())),
                SyntaxFactory.Interpolation(
                    SyntaxFactory.InvocationExpression(
                        SyntaxFactory.IdentifierName("nameof"),
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.IdentifierName(identifierName)))))),
                SyntaxFactory.InterpolatedStringText()
                    .WithTextToken(SyntaxFactory.Token(
                        SyntaxFactory.TriviaList(),
                        SyntaxKind.InterpolatedStringTextToken,
                        "'.",
                        "'.",
                        SyntaxFactory.TriviaList()))
            }));
    }

    private static InterpolatedStringExpressionSyntax CreateExpressionMessageExpression(
        string expectedValue)
    {
        // Create: $"Expected to get {expectedValue} for the expression."
        // Since there's no interpolation needed, we could use a regular string,
        // but using interpolated string for consistency
        return SyntaxFactory.InterpolatedStringExpression(
            SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken),
            SyntaxFactory.SingletonList<InterpolatedStringContentSyntax>(
                SyntaxFactory.InterpolatedStringText()
                    .WithTextToken(SyntaxFactory.Token(
                        SyntaxFactory.TriviaList(),
                        SyntaxKind.InterpolatedStringTextToken,
                        $"Expected to get {expectedValue} for the expression.",
                        $"Expected to get {expectedValue} for the expression.",
                        SyntaxFactory.TriviaList()))));
    }
}
