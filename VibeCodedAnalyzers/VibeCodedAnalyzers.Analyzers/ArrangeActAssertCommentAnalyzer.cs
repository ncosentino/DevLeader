using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace VibeCodedAnalyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class ArrangeActAssertCommentAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "VC003";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "Do not use Arrange/Act/Assert comments in tests",
            messageFormat: "Remove Arrange/Act/Assert comment from test",
            category: "Testing",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.MethodDeclaration);
        }

        private static void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var method = (MethodDeclarationSyntax)context.Node;

            // Only analyze test methods (xUnit [Fact] or [Theory])
            var hasTestAttribute = method.AttributeLists
                .SelectMany(a => a.Attributes)
                .Any(attr =>
                {
                    var name = attr.Name.ToString();
                    return name.EndsWith("Fact") || name.EndsWith("Theory") || name.EndsWith("Xunit.Fact") || name.EndsWith("Xunit.Theory");
                });

            if (!hasTestAttribute)
            {
                return;
            }

            // Detect AAA comments in single-line and block (multi-line) comments
            bool HasAAAComment(SyntaxTrivia trivia)
            {
                var kind = trivia.Kind();
                if (!(kind == SyntaxKind.SingleLineCommentTrivia || kind == SyntaxKind.MultiLineCommentTrivia))
                    return false;

                var text = trivia.ToString().ToLowerInvariant();
                return text.Contains("arrange") || text.Contains("act") || text.Contains("assert");
            }

            void ReportForTrivia(SyntaxTrivia trivia)
            {
                var diagnostic = Diagnostic.Create(Rule, Location.Create(context.Node.SyntaxTree, trivia.Span));
                context.ReportDiagnostic(diagnostic);
            }

            foreach (var trivia in method.GetLeadingTrivia())
            {
                if (HasAAAComment(trivia))
                {
                    ReportForTrivia(trivia);
                }
            }

            if (method.Body != null)
            {
                // Leading trivia on statements
                foreach (var statement in method.Body.Statements)
                {
                    foreach (var trivia in statement.GetLeadingTrivia())
                    {
                        if (HasAAAComment(trivia))
                        {
                            ReportForTrivia(trivia);
                        }
                    }

                    // Trailing trivia may contain inline end-of-line comments
                    foreach (var trivia in statement.GetTrailingTrivia())
                    {
                        if (HasAAAComment(trivia))
                        {
                            ReportForTrivia(trivia);
                        }
                    }
                }
            }
            else if (method.ExpressionBody != null)
            {
                foreach (var trivia in method.ExpressionBody.GetLeadingTrivia())
                {
                    if (HasAAAComment(trivia))
                    {
                        ReportForTrivia(trivia);
                    }
                }
            }
        }
    }
}
