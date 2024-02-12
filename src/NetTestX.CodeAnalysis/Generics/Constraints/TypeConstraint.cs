using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class TypeConstraint(ITypeSymbol constraintType) : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type) => constraintType switch
    {
        { TypeKind: TypeKind.Interface } => type.ImplementsInterface((INamedTypeSymbol)constraintType),
        _ => type.IsInheritedFrom(constraintType)
    };
}
