using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

/// <summary>
/// <see cref="IMockValueProvider"/> for NSubstitute
/// </summary>
public class NSubstituteValueProvider : MockValueProviderBase
{
    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"Substitute.For<{type.ToDisplayString(CommonFormats.ShortNullableFormat)}>()";

        return base.Resolve(type);
    }

    public override IEnumerable<string> CollectNamespaces() => base.CollectNamespaces().Union(["NSubstitute"]);
}
