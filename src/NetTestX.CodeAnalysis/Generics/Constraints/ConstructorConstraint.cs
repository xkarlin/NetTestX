using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints;

internal class ConstructorConstraint : IConstraint
{
    public bool IsSatisfiedBy(ITypeSymbol type)
    {
        if (type.Kind != SymbolKind.NamedType)
            return false;

        var namedType = (INamedTypeSymbol)type;

        foreach (var constructor in namedType.Constructors)
        {
            if (constructor.IsStatic)
                continue;

            if (constructor.DeclaredAccessibility != Accessibility.Public)
                continue;

            if (constructor.Parameters.Length != 0)
                continue;

            return true;
        }

        return false;
    }
}
