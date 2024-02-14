using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record DisposableTypeMethodBodyModel(IMethodSymbol Constructor, bool IsAsync) : IMethodBodyModel
{
    public TestMethodModelBase Parent { get; set; }

    public INamedTypeSymbol Type => Parent.Parent.Type;

    public string DisposableInterface => IsAsync ? nameof(IAsyncDisposable) : nameof(IDisposable);

    public IEnumerable<string> CollectNamespaces() => [typeof(IDisposable).Namespace];

    public string GetDisplayName() => $"{DisposableInterface} interface";
}
