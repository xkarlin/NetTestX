using System.Collections.Generic;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Templates;

public record TestClassModel(
    string TestClassName,
    string TestClassNamespace,
    IEnumerable<ITestMethodModel> TestMethods);
