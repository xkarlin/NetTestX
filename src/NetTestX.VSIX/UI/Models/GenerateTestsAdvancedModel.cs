using NetTestX.CodeAnalysis.Templates.TestMethods;
using System.Collections.Generic;

namespace NetTestX.VSIX.UI.Models;

public class GenerateTestsAdvancedModel
{
    public string TestProject { get; set; }

    public string TestFileName { get; set; }

    public string TestClassName { get; set; }

    public string TestClassNamespace { get; set; }

    public Dictionary<TestMethodModelBase, bool> TestMethodMap { get; set; }
}
