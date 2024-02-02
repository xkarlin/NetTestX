using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public class NUnitFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => null;

    public string TestMethodAttribute => "Test";

    public string AssertTrue(string expression) => $"Assert.That({expression})";

    public IEnumerable<string> CollectNamespaces() => ["NUnit.Framework"];
}
