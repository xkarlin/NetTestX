using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates;

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
}
