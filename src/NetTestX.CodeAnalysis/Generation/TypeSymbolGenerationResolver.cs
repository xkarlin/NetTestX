using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generics;

namespace NetTestX.CodeAnalysis.Generation;

internal static class TypeSymbolGenerationResolver
{
    public static INamedTypeSymbol Resolve(INamedTypeSymbol type, Compilation compilation)
    {
        if (type.IsGenericType && SymbolEqualityComparer.Default.Equals(type, type.OriginalDefinition))
            type = GenericTypeResolver.Resolve(type, compilation);

        return type;
    }
}
