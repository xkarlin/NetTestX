using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System;
using System.Linq;

namespace NetTestX.CodeAnalysis.Templates.Utils;

public static class RazorTemplateHelpers
{
    public static string Full(ISymbol symbol)
        => symbol.ToDisplayString(CommonFormats.FullNullableFormat);

    public static string Short(ISymbol symbol)
        => symbol.ToDisplayString(CommonFormats.ShortNullableFormat);

    public static string Args(IMethodSymbol method)
        => string.Join(", ", method.Parameters.Select(x => Arg(x, x => x)));

    public static string Args(IMethodSymbol method, Func<string, string> transform)
        => string.Join(", ", method.Parameters.Select(x => Arg(x, transform)));

    public static string Arg(IParameterSymbol param, Func<string, string> transform)
    {
        if (param.RefKind == RefKind.None)
            return transform.Invoke(param.Name);

        string @ref = param.RefKind switch
        {
            RefKind.In => "in",
            RefKind.Ref => "ref",
            RefKind.Out => "out",
            _ => throw new ArgumentOutOfRangeException(nameof(param))
        };

        return $"{@ref} {transform.Invoke(param.Name)}";
    }

    public static string Pascal(string value) => char.ToUpper(value[0]) + value[1..];

    public static string TestPascal(string value) => $"test{Pascal(value)}";
}
