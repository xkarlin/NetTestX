using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class ReferenceTypeConstraint : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => type.IsReferenceType;
}
