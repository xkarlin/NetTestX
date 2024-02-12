using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal interface IConstraint
{
    bool IsSatisfiedBy(ITypeSymbol type);
}
