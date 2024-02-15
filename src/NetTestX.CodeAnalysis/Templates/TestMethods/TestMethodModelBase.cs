using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public abstract class TestMethodModelBase : INamespaceCollector
{
    public string MethodName { get; }

    public ISymbol Symbol { get; }

    public IMethodBodyModel MethodBodyModel { get; }

    public TestClassModel Parent { get; set; }

    public bool IncludeAAAComments => (Parent.AdvancedOptions & AdvancedGeneratorOptions.IncludeAAAComments) != 0;

    protected TestMethodModelBase(ISymbol symbol, IMethodBodyModel methodBodyModel, string methodName)
    {
        Symbol = symbol;
        MethodBodyModel = methodBodyModel;
        methodBodyModel.Parent = this;
        MethodName = methodName ?? $"Test{symbol.Name}";
    }

    public virtual IEnumerable<string> CollectNamespaces() => MethodBodyModel.CollectNamespaces();
}
