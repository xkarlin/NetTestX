using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public class TestMethodModel : ITestMethodModel
{
    public ISymbol Symbol { get; }

    public IMethodBodyModel MethodBodyModel { get; }

    public TestClassModel Parent { get; set; }

    public TestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel)
    {
        Symbol = symbol;
        MethodBodyModel = methodBodyModel;
        methodBodyModel.Parent = this;
    }

    public IEnumerable<string> CollectNamespaces() => MethodBodyModel.CollectNamespaces().Union(["Xunit"]);
}
