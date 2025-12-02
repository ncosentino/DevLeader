using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace VibeCodedAnalyzers.RangeActAssert;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RangeActAssertCommentAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "VCAA001";

    private static readonly LocalizableString Title = "Disallowed test scaffold comment";
    private static readonly LocalizableString MessageFormat = "Remove '{0}' comment from tests";
    private static readonly LocalizableString Description = "Range/Act/Assert comments are disallowed in tests; remove them to compile.";
    private const string Category = "Testing";

    private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: Description);

    private static readonly ImmutableArray<string> BlockedPhrases = ImmutableArray.Create(
        "Arrange",
        "Act",
        "Assert");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxTreeAction(AnalyzeSyntaxTree);
    }

    private static void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context)
    {
        var text = context.Tree.GetText(context.CancellationToken);
        foreach (var line in text.Lines)
        {
            var span = line.Span;
            var lineText = text.ToString(span);

            // Check for single-line comment markers
            int idx = lineText.IndexOf("//");
            if (idx >= 0)
            {
                var comment = lineText.Substring(idx + 2).Trim();
                foreach (var phrase in BlockedPhrases)
                {
                    if (comment.StartsWith(phrase, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var diagnostic = Diagnostic.Create(
                            Rule,
                            Location.Create(context.Tree, new TextSpan(line.Start + idx, lineText.Length - idx)),
                            phrase);
                        context.ReportDiagnostic(diagnostic);
                        break;
                    }
                }
            }

            // Check for block comments spanning within a line start
            int blockIdx = lineText.IndexOf("/*");
            if (blockIdx >= 0)
            {
                var after = lineText.Substring(blockIdx + 2);
                foreach (var phrase in BlockedPhrases)
                {
                    if (after.IndexOf(phrase, System.StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var diagnostic = Diagnostic.Create(
                            Rule,
                            Location.Create(context.Tree, new TextSpan(line.Start + blockIdx, lineText.Length - blockIdx)),
                            phrase);
                        context.ReportDiagnostic(diagnostic);
                        break;
                    }
                }
            }
        }
    }
}
