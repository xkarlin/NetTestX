﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Community.VisualStudio.Toolkit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.VSIX.Options;

namespace NetTestX.VSIX.Commands.Helpers;

/// <summary>
/// Helper class for <see cref="ISymbol"/>s and derived types
/// </summary>
public static class SymbolHelper
{
    /// <summary>
    /// Find all available <see cref="INamedTypeSymbol"/> that can be generated from the given <paramref name="node"/>
    /// </summary>
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

    /// <summary>
    /// Whether the tests can be generated for the given <paramref name="typeSymbol"/>
    /// </summary>
    public static bool CanGenerateTestsForTypeSymbol(INamedTypeSymbol typeSymbol)
    {
        var generalOptions = GeneralOptions.Instance;

        Accessibility targetAccessibility = generalOptions.TestInternalMembers ? Accessibility.Internal : Accessibility.Public;

        if (typeSymbol.GetEffectiveAccessibility() < targetAccessibility)
            return false;

        if (typeSymbol.TypeKind is not TypeKind.Class and not TypeKind.Struct)
            return false;

        if (typeSymbol.IsAbstract)
            return false;

        return true;
    }

    /// <summary>
    /// Show the warning that multiple test classes will be generated for the given file
    /// </summary>
    public static bool ShowMultipleTypesWarning(string fileName, ImmutableArray<INamedTypeSymbol> availableTypes)
    {
        string typesString = string.Join("\n", availableTypes.Select(x => x.Name));
        return VS.MessageBox.ShowConfirm("NetTestX - multiple types found", $"The selected file {fileName} contains multiple types:\n\n{typesString}\n\nWould you like to proceed and generate tests for all these types?");
    }
}
