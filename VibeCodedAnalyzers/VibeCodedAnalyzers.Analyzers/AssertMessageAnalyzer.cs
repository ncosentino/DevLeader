using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace VibeCodedAnalyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AssertMessageAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticIdTrue = "VC001";
        public const string DiagnosticIdFalse = "VC002";

        private static readonly DiagnosticDescriptor RuleTrue = new DiagnosticDescriptor(
            id: DiagnosticIdTrue,
            title: "Assert.True requires message",
            messageFormat: "Provide a descriptive message as the second parameter when using Assert.True",
            category: "Testing",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor RuleFalse = new DiagnosticDescriptor(
            id: DiagnosticIdFalse,
            title: "Assert.False requires message",
            messageFormat: "Provide a descriptive message as the second parameter when using Assert.False",
            category: "Testing",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(RuleTrue, RuleFalse); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
        {
            var invocation = context.Node as InvocationExpressionSyntax;
            if (invocation == null) return;

            var memberAccess = invocation.Expression as MemberAccessExpressionSyntax;
            if (memberAccess == null) return;

            var methodName = memberAccess.Name.Identifier.Text;
            if (!(methodName == "True" || methodName == "False")) return;

            var symbolInfo = context.SemanticModel.GetSymbolInfo(memberAccess);
            var symbol = symbolInfo.Symbol as IMethodSymbol;
            if (symbol == null) return;

            // Ensure it's Xunit.Assert.True/False
            var containing = symbol.ContainingType;
            if (containing == null) return;
            var ns = containing.ContainingNamespace != null ? containing.ContainingNamespace.ToDisplayString() : null;
            if (containing.Name != "Assert" || ns != "Xunit") return;

            var argList = invocation.ArgumentList;
            var argCount = argList != null ? argList.Arguments.Count : 0;
            if (argCount < 2)
            {
                var descriptor = methodName == "True" ? RuleTrue : RuleFalse;
                var diagnostic = Diagnostic.Create(descriptor, invocation.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
