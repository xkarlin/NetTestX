using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record AccessibleInstanceMethodBodyModel(IMethodSymbol Method) : IMethodBodyModel
{
    public TestMethodModelBase Parent { get; set; }

    public IEnumerable<string> CollectNamespaces()
    {
        IEnumerable<string> namespaces = [];

        foreach (var param in Method.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());

        return namespaces;
    }
}
