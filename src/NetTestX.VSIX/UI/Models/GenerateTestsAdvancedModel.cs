using NetTestX.CodeAnalysis.Templates.TestMethods;
using System.Collections.Generic;
using NetTestX.CodeAnalysis;

namespace NetTestX.VSIX.UI.Models;

public class GenerateTestsAdvancedModel
{
    public string TestProject { get; set; }

    public string TestFileName { get; set; }

    public string TestClassName { get; set; }

    public string TestClassNamespace { get; set; }

    public Dictionary<TestMethodModelBase, bool> TestMethodMap { get; set; }
 
    public AdvancedGeneratorOptions AdvancedOptions { get; set; }
}
