using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Common;

/// <summary>
/// Comparer for <see cref="ISymbol"/>s that uses their name for comparison
/// </summary>
public class SymbolNameComparer : IEqualityComparer<ISymbol>
{
    public static SymbolNameComparer Default { get; } = new();

    internal SymbolNameComparer() { }

    public bool Equals(ISymbol x, ISymbol y) => x.ToDisplayString(CommonFormats.FullNullableFormat) == y.ToDisplayString(CommonFormats.FullNullableFormat);

    public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
}
