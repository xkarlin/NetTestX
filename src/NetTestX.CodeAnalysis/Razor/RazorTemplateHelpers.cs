using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System;
using System.Linq;

namespace NetTestX.CodeAnalysis.Razor;

internal static class RazorTemplateHelpers
{
    public static string Full(ISymbol symbol)
        => symbol.ToDisplayString(CommonFormats.FullNullableFormat);

    public static string Args(IMethodSymbol method)
        => string.Join(", ", method.Parameters.Select(Arg));

    public static string Arg(IParameterSymbol param)
    {
        if (param.RefKind == RefKind.None)
            return param.Name;

        string prefix = param.RefKind switch
        {
            RefKind.In => "in",
            RefKind.Ref => "ref",
            RefKind.Out => "out",
            _ => throw new ArgumentOutOfRangeException(nameof(param))
        };

        return $"{prefix} {param.Name}";
    }
}
