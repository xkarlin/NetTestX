using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public interface ITestMethodModel
{
    ISymbol Symbol { get; }

    IMethodBodyModel MethodBodyModel { get; }

    TestClassModel Parent { get; set; }
}
