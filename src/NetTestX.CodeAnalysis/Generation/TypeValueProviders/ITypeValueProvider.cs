using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

public interface ITypeValueProvider : INamespaceCollector
{
    string Resolve(ITypeSymbol type);
}
