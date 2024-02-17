using Microsoft.CodeAnalysis;
using System;

namespace NetTestX.VSIX.Options.Parsing;

public static class OptionResolverHelper
{
    public static string ResolveGeneralOption(string text, INamedTypeSymbol type)
    {
        GeneralOptionsVariablesProvider provider = new(type);
        OptionResolver resolver = new(provider);
        return resolver.Resolve(text);
    }
}
