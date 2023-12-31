using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.DependencyResolvers;

public interface ITypeValueProvider
{
    string Resolve(ITypeSymbol type);
}
