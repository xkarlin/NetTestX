using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

public interface ITypeValueProvider
{
    string Resolve(ITypeSymbol type);
}
