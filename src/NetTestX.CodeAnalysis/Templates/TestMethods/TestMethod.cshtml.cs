using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public record TestMethodModel(ISymbol Symbol, object MethodBodyModel)
    : ITestMethodModel;
