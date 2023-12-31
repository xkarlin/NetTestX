using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public record AsyncTestMethodModel(ISymbol Symbol, object MethodBodyModel)
    : ITestMethodModel;