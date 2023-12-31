using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System;
using System.Linq;

namespace NetTestX.CodeAnalysis.Razor;

internal static class RazorTemplateHelpers
{
    public static string Full(ISymbol symbol)
        => symbol.ToDisplayString(CommonFormats.FullNullableFormat);

    public static string Args(IMethodSymbol method, string prefix = "")
        => string.Join(", ", method.Parameters.Select(x => Arg(x, prefix)));

    public static string Arg(IParameterSymbol param, string prefix = "")
    {
        if (param.RefKind == RefKind.None)
            return prefix + param.Name;

        string @ref = param.RefKind switch
        {
            RefKind.In => "in",
            RefKind.Ref => "ref",
            RefKind.Out => "out",
            _ => throw new ArgumentOutOfRangeException(nameof(param))
        };

        return $"{@ref} {prefix}{param.Name}";
    }
}
