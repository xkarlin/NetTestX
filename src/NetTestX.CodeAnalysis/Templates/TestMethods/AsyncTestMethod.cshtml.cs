using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates;
using System.Collections.Generic;
using System.Linq;

public class AsyncTestMethodModel : ITestMethodModel
{
    public ISymbol Symbol { get; }

    public IMethodBodyModel MethodBodyModel { get; }

    public TestClassModel Parent { get; set; }

    public AsyncTestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel)
    {
        Symbol = symbol;
        MethodBodyModel = methodBodyModel;
        methodBodyModel.Parent = this;
    }

    public IEnumerable<string> CollectNamespaces() => MethodBodyModel.CollectNamespaces().Union(["Xunit"]);
}
