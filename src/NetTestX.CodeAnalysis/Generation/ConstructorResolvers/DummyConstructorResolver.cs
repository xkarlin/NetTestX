﻿using System;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers;

public class DummyConstructorResolver : IConstructorResolver
{
    public IMethodSymbol Resolve(INamedTypeSymbol type)
    {
        if (!type.TryGetAccessibleConstructor(Accessibility.Public, out var constructor))
            throw new InvalidOperationException($"Could not find any constructor for the type {type.Name}");

        return constructor;
    }
}
