using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class EqualsTypeConstraint(ITypeSymbol expectedType) : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => SymbolNameComparer.Default.Equals(expectedType, type);
}
