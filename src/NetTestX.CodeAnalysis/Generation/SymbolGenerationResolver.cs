using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generics;

namespace NetTestX.CodeAnalysis.Generation;

internal static class SymbolGenerationResolver
{
    public static INamedTypeSymbol Resolve(INamedTypeSymbol type, Compilation compilation)
    {
        if (type.IsGenericTypeDefinition())
            type = GenericTypeResolver.Resolve(type, compilation);

        return type;
    }

    public static IMethodSymbol Resolve(IMethodSymbol method, Compilation compilation)
    {
        if (method.IsGenericMethodDefinition())
            method = GenericTypeResolver.Resolve(method, compilation);

        return method;
    }
}
