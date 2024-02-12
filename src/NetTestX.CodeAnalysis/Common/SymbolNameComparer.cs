using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Common;

public class SymbolNameComparer : IEqualityComparer<ISymbol>
{
    public static SymbolNameComparer Default { get; } = new();

    private SymbolNameComparer() { }

    public bool Equals(ISymbol x, ISymbol y) => x.ToDisplayString(CommonFormats.FullNullableFormat) == y.ToDisplayString(CommonFormats.FullNullableFormat);

    public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
}
