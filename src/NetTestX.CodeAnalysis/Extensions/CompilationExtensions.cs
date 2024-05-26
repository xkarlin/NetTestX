using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace NetTestX.CodeAnalysis.Extensions;

/// <summary>
/// Extensions for <see cref="Compilation"/> and <see cref="CSharpCompilation"/>
/// </summary>
public static class CompilationExtensions
{
    /// <inheritdoc cref="GetTypeSymbol{T}(Compilation)" />
    public static INamedTypeSymbol GetNamedSymbol<T>(this Compilation compilation)
        => (INamedTypeSymbol)compilation.GetTypeSymbol<T>();

    /// <inheritdoc cref="GetTypeSymbol(Compilation, Type)" />
    public static INamedTypeSymbol GetNamedSymbol(this Compilation compilation, Type type)
        => (INamedTypeSymbol)compilation.GetTypeSymbol(type);

    /// <summary>
    /// Retrieve a type symbol for the given type <typeparamref name="T"/>
    /// </summary>
    public static ITypeSymbol GetTypeSymbol<T>(this Compilation compilation)
        => compilation.GetTypeSymbol(typeof(T));

    /// <summary>
    /// Retrieve a type symbol for the given <paramref name="type"/>
    /// </summary>
    public static ITypeSymbol GetTypeSymbol(this Compilation compilation, Type type)
        => compilation.GetTypeByMetadataName(type.FullName);
}
