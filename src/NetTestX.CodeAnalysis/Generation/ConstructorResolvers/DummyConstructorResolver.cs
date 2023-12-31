using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers;

public class DummyConstructorResolver : IConstructorResolver
{
    public IMethodSymbol Resolve(INamedTypeSymbol type) => type.Constructors[0];
}
