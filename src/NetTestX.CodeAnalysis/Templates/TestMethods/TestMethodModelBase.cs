using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public abstract class TestMethodModelBase : INamespaceCollector
{
    public ISymbol Symbol { get; }

    public IMethodBodyModel MethodBodyModel { get; }

    public TestClassModel Parent { get; set; }

    protected TestMethodModelBase(ISymbol symbol, IMethodBodyModel methodBodyModel)
    {
        Symbol = symbol;
        MethodBodyModel = methodBodyModel;
        methodBodyModel.Parent = this;
    }

    public virtual IEnumerable<string> CollectNamespaces() => MethodBodyModel.CollectNamespaces().Union(["Xunit"]);
}
