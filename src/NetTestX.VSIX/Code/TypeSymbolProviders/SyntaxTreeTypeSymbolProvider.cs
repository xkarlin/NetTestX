using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NetTestX.VSIX.Code.TypeSymbolProviders;

public class SyntaxTreeTypeSymbolProvider(Compilation compilation, SyntaxTree syntaxTree) : ITypeSymbolProvider
{
    public async Task<INamedTypeSymbol> GetTypeSymbolAsync()
    {
        var syntaxRoot = await syntaxTree.GetRootAsync();

        var typeDeclaration = syntaxRoot
            .DescendantNodes(node => node is not TypeDeclarationSyntax)
            .OfType<TypeDeclarationSyntax>()
            .First();

        var typeSymbol = compilation.GetSemanticModel(syntaxTree).GetDeclaredSymbol(typeDeclaration);

        return typeSymbol;
    }
}
