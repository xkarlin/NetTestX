using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

/// <summary>
/// <see cref="IMockValueProvider"/> for Moq
/// </summary>
public class MoqValueProvider : MockValueProviderBase
{
    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"new Mock<{type.ToDisplayString(CommonFormats.ShortNullableFormat)}>().Object";

        return base.Resolve(type);
    }

    public override IEnumerable<string> CollectNamespaces() => base.CollectNamespaces().Union(["Moq"]);
}
