using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public class AsyncTestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel, string methodName = null)
    : TestMethodModelBase(symbol, methodBodyModel, methodName);
