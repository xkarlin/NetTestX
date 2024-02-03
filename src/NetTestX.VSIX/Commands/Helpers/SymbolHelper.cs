using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Community.VisualStudio.Toolkit;
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
            .Where(CanGenerateTestsForTypeSymbol);

        return typeSymbols;
    }

    public static bool CanGenerateTestsForTypeSymbol(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeKind is not TypeKind.Class and not TypeKind.Struct)
            return false;

        if (typeSymbol.IsAbstract)
            return false;

        return true;
    }

    public static bool ShowMultipleTypesWarning(string fileName, ImmutableArray<INamedTypeSymbol> availableTypes)
    {
        string typesString = string.Join("\n", availableTypes.Select(x => x.Name));
        return VS.MessageBox.ShowConfirm("NetTestX - multiple types found", $"The selected file {fileName} contains multiple types:\n\n{typesString}\n\nWould you like to proceed and generate tests for all these types?");
    }
}
