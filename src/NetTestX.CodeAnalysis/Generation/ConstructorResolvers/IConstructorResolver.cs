using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers;

/// <summary>
/// Represents a strategy to retrieve constructors for named types
/// </summary>
public interface IConstructorResolver
{
    /// <summary>
    /// Retrieve a constructor <see cref="IMethodSymbol"/> for the given <paramref name="type"/>
    /// </summary>
    IMethodSymbol Resolve(INamedTypeSymbol type);
}
