using Microsoft.CodeAnalysis;
using System;

namespace NetTestX.VSIX.Options.Parsing;

/// <summary>
/// Helper class used to resolve strings that contain option variables
/// </summary>
public static class OptionResolverHelper
{
    /// <summary>
    /// Resolve an option contained within <see cref="GeneralOptions"/>
    /// </summary>
    public static string ResolveGeneralOption(string text, INamedTypeSymbol type)
    {
        GeneralOptionsVariablesProvider provider = new(type);
        OptionResolver resolver = new(provider);
        return resolver.Resolve(text);
    }
}
