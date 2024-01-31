using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NetTestX.VSIX.Commands.Helpers;

public static class SymbolHelper
{
    public static IEnumerable<INamedTypeSymbol> GetAvailableTypeSymbolsForGeneration(SyntaxNode node, Compilation compilation)
    {
        var semanticModel = compilation.GetSemanticModel(node.SyntaxTree);

        var typeDeclarations = node
            .DescendantNodesAndSelf(x => x is not TypeDeclarationSyntax)
            .Where(x => (SyntaxKind)x.RawKind is
                SyntaxKind.ClassDeclaration or
                SyntaxKind.StructDeclaration or
                SyntaxKind.RecordDeclaration or
                SyntaxKind.RecordStructDeclaration);

        var typeSymbols = typeDeclarations
            .Select(x => semanticModel.GetDeclaredSymbol((TypeDeclarationSyntax)x))
            .Where(IsTypeSymbolAvailable);

        return typeSymbols;
    }

    private static bool IsTypeSymbolAvailable(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.IsAbstract)
            return false;

        return true;
    }
}
