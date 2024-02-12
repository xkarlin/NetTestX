using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class UnmanagedTypeConstraint : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => type.IsUnmanagedType;
}
