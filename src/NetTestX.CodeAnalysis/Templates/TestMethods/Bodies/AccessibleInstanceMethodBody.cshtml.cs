using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public record AccessibleInstanceMethodBodyModel(IMethodSymbol Method) : IMethodBodyModel
{
    public ITestMethodModel Parent { get; set; }
}
