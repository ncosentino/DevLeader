using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VibeCodedAnalyzers.RangeActAssert;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class AssertMessageRequiredAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "VCAA002";

    private static readonly LocalizableString Title = "Assert.True/False must include message";
    private static readonly LocalizableString MessageFormat = "Provide a helpful/descriptive string message in Assert.{0} call";
    private static readonly LocalizableString Description = "XUnit Assert.True and Assert.False calls must include a helpful/descriptive message parameter.";
    private const string Category = "Testing";

    private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        var invocation = (InvocationExpressionSyntax)context.Node;
        if (invocation.Expression is not MemberAccessExpressionSyntax memberAccess)
            return;

        var methodName = memberAccess.Name.Identifier.Text;
        if (methodName is not ("True" or "False"))
            return;

        // Verify the receiver is Assert (qualifier could be simple or qualified)
        var symbolInfo = context.SemanticModel.GetSymbolInfo(memberAccess.Expression, context.CancellationToken);
        var receiverSymbol = symbolInfo.Symbol ?? (symbolInfo.CandidateSymbols.Length == 1 ? symbolInfo.CandidateSymbols[0] : null);
        if (receiverSymbol is INamedTypeSymbol typeSymbol)
        {
            if (typeSymbol.Name != "Assert")
                return;
        }
        else
        {
            // Fallback: check identifier text
            if (memberAccess.Expression is IdentifierNameSyntax id && id.Identifier.Text != "Assert")
                return;
        }

        var argList = invocation.ArgumentList;
        if (argList == null)
            return;

        // Report when only 1 argument is provided (the condition) OR when second arg exists but is not string
        if (argList.Arguments.Count == 1)
        {
            Report(context, invocation, methodName);
            return;
        }

        if (argList.Arguments.Count >= 2)
        {
            var secondArg = argList.Arguments[1].Expression;
            var argType = context.SemanticModel.GetTypeInfo(secondArg, context.CancellationToken).ConvertedType;
            if (argType == null || argType.SpecialType != SpecialType.System_String)
            {
                Report(context, invocation, methodName);
            }
        }
    }

    private static void Report(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, string methodName)
    {
        var diagnostic = Diagnostic.Create(
            Rule,
            invocation.GetLocation(),
            methodName);
        context.ReportDiagnostic(diagnostic);
    }
}
