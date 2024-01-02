using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record AccessibleStaticMethodBodyModel(IMethodSymbol Method) : IMethodBodyModel
{
    public TestMethodModelBase Parent { get; set; }

    public INamedTypeSymbol Type => Parent.Parent.Type;

    public IEnumerable<string> CollectNamespaces()
    {
        IEnumerable<string> namespaces = [];

        foreach (var param in Method.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());

        return namespaces;
    }
}
