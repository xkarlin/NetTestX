using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

[TypeValueProvider(MockingLibrary.NSubstitute)]
public class NSubstituteValueProvider : TypeValueProviderBase
{
    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"Substitute.For<{type.ToDisplayString(CommonFormats.ShortNullableFormat)}>()";

        return base.Resolve(type);
    }

    public override IEnumerable<string> CollectNamespaces() => base.CollectNamespaces().Union(["NSubstitute"]);
}
