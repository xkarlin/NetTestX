using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record AccessibleInstanceMethodBodyModel(IMethodSymbol Method, IMethodSymbol Constructor) : IMethodBodyModel
{
    public TestMethodModelBase Parent { get; set; }

    public INamedTypeSymbol Type => Parent.Parent.Type;

    public IEnumerable<string> CollectNamespaces()
    {
        IEnumerable<string> namespaces = [];

        foreach (var param in Constructor.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());

        foreach (var param in Method.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());
        return namespaces;
    }

    public string GetDisplayName() => Method.ToDisplayString(CommonFormats.ShortNullableFormat);
}
