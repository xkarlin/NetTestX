using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Utils;

internal class TypeSymbolEnumerableVisitor : SymbolVisitor<IEnumerable<INamedTypeSymbol>>
{
    public override IEnumerable<INamedTypeSymbol> VisitNamespace(INamespaceSymbol symbol) => symbol.GetMembers().SelectMany(Visit);

    public override IEnumerable<INamedTypeSymbol> VisitNamedType(INamedTypeSymbol symbol) => [symbol];
}
