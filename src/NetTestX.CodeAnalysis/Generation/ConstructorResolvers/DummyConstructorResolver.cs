using System;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers;

/// <summary>
/// A constructor resolver that takes any accessible constructor that is available
/// taking into account <paramref name="accessibility"/>
/// </summary>
public class DummyConstructorResolver(Accessibility accessibility) : IConstructorResolver
{
    public IMethodSymbol Resolve(INamedTypeSymbol type)
    {
        if (!type.TryGetAccessibleConstructor(accessibility, out var constructor))
            throw new InvalidOperationException($"Could not find any constructor for the type {type.Name}");

        return constructor;
    }
}
