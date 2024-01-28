using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public class AsyncTestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel)
    : TestMethodModelBase(symbol, methodBodyModel)
{ }
