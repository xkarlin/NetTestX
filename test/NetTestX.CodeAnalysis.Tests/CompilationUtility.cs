using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace NetTestX.CodeAnalysis.Tests;

internal static class CompilationUtility
{
    public static readonly MetadataReference[] DefaultReferences =
    [
        MetadataReference.CreateFromFile(typeof(int).Assembly.Location)
    ];

    public static CSharpCompilation CreateCompilation(string sourceText, params MetadataReference[] references)
    {
        var tree = CSharpSyntaxTree.ParseText(sourceText);

        var compilation = CSharpCompilation.Create("Test", syntaxTrees: [tree], references: DefaultReferences.Concat(references));

        return compilation;
    }
}
