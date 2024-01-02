using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

public interface IMockValueProvider : INamespaceCollector
{
    string Resolve(ITypeSymbol type);
}
