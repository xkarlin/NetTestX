using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using NetTestX.CodeAnalysis.Templates.TestMethods;

public class AsyncTestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel)
    : TestMethodModelBase(symbol, methodBodyModel)
{ }
