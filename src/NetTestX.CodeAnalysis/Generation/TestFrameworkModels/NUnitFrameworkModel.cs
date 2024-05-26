using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

/// <summary>
/// <see cref="ITestFrameworkModel"/> for NUnit
/// </summary>
public class NUnitFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => null;

    public string TestMethodAttribute => "Test";

    public string AssertTrue(string expression) => $"Assert.That({expression})";

    public IEnumerable<string> CollectNamespaces() => ["NUnit.Framework"];
}
