using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

internal class FakeItEasyValueProvider : MockValueProviderBase
{
    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"A.Fake<{type.ToDisplayString(CommonFormats.ShortNullableFormat)}>()";

        return base.Resolve(type);
    }

    public override IEnumerable<string> CollectNamespaces() => base.CollectNamespaces().Union(["FakeItEasy"]);
}
