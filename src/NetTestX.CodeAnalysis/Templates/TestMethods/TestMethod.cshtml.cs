using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Templates.TestMethods;

public class TestMethodModel(ISymbol symbol, IMethodBodyModel methodBodyModel)
    : TestMethodModelBase(symbol, methodBodyModel)
{ }
