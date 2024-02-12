using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class ValueTypeConstraint : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => type.IsValueType;
}
