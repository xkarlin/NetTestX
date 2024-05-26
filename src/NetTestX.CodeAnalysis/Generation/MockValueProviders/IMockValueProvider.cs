using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

/// <summary>
/// Represents a provider of mocked values
/// </summary>
public interface IMockValueProvider : INamespaceCollector
{
    /// <summary>
    /// Resolve a mock instance for the given <paramref name="type"/> into a string
    /// </summary>
    string Resolve(ITypeSymbol type);
}
