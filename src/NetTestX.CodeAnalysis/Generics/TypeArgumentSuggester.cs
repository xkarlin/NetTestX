using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetTestX.CodeAnalysis.Generics;

internal static class TypeArgumentSuggester
{
    private static readonly ConditionalWeakTable<Compilation, INamedTypeSymbol[]> _wellKnownTypesCache = new();

    private static readonly ImmutableArray<Type> _wellKnownTypes = ImmutableArray.Create(
    [
        typeof(object),
        typeof(int),
        typeof(List<>),
        typeof(HashSet<>),
        typeof(Dictionary<,>)
    ]);

    public static IEnumerable<INamedTypeSymbol> EnumerateSuggestions(Compilation compilation)
    {
        if (!_wellKnownTypesCache.TryGetValue(compilation, out var wellKnownTypeSymbols))
        {
            wellKnownTypeSymbols = _wellKnownTypes.Select(compilation.GetNamedSymbol).ToArray();
            _wellKnownTypesCache.Add(compilation, wellKnownTypeSymbols);
        }

        IEnumerable<INamedTypeSymbol> suggestions = wellKnownTypeSymbols;

        TypeSymbolEnumerableVisitor visitor = new();

        suggestions = suggestions.Concat(visitor.Visit(compilation.Assembly.GlobalNamespace));

        suggestions = suggestions.Concat(visitor
            .Visit(compilation.GlobalNamespace)
            .Where(s => !SymbolEqualityComparer.Default.Equals(compilation.Assembly, s.ContainingAssembly)));

        return suggestions;
    }
}
