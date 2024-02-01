using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record AccessibleIndexerMethodBodyModel(IPropertySymbol Indexer, IMethodSymbol Constructor) : IMethodBodyModel
{
    public TestMethodModelBase Parent { get; set; }

    public INamedTypeSymbol Type => Parent.Parent.Type;

    public bool GetterAccessible => Indexer.GetMethod is { DeclaredAccessibility: Accessibility.Public };

    public bool SetterAccessible => Indexer.SetMethod is { DeclaredAccessibility: Accessibility.Public };

    public IEnumerable<string> CollectNamespaces()
    {
        IEnumerable<string> namespaces = [];

        foreach (var param in Constructor.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());

        foreach (var param in Indexer.Parameters)
            namespaces = namespaces.Union(param.Type.CollectNamespaces());

        return namespaces;
    }
}
