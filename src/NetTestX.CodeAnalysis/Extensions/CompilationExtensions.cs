using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Extensions;

public static class CompilationExtensions
{
    public static INamedTypeSymbol GetNamedSymbol<T>(this Compilation compilation)
        => (INamedTypeSymbol)compilation.GetTypeSymbol<T>();

    public static INamedTypeSymbol GetNamedSymbol(this Compilation compilation, Type type)
        => (INamedTypeSymbol)compilation.GetTypeSymbol(type);

    public static ITypeSymbol GetTypeSymbol<T>(this Compilation compilation)
        => compilation.GetTypeSymbol(typeof(T));

    public static ITypeSymbol GetTypeSymbol(this Compilation compilation, Type type)
        => compilation.GetTypeByMetadataName(type.FullName);
}
