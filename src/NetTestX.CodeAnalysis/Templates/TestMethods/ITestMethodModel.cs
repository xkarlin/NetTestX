using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public interface ITestMethodModel
{
    ISymbol Symbol { get; }

    object MethodBodyModel { get; }
}
