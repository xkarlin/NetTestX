using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

/// <summary>
/// <see cref="ITestFrameworkModel"/> for xUnit
/// </summary>
public class XUnitFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => null;

    public string TestMethodAttribute => "Fact";

    public string AssertTrue(string expression) => $"Assert.True({expression})";

    public IEnumerable<string> CollectNamespaces() => ["Xunit"];
}
