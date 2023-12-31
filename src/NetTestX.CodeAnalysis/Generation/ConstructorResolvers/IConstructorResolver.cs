using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers;

public interface IConstructorResolver
{
    IMethodSymbol Resolve(INamedTypeSymbol type);
}
