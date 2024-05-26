using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

/// <summary>
/// <see cref="ITestFrameworkModel"/> for MSTest
/// </summary>
public class MSTestFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => "TestClass";

    public string TestMethodAttribute => "TestMethod";

    public string AssertTrue(string expression) => $"Assert.IsTrue({expression})";

    public IEnumerable<string> CollectNamespaces() => ["Microsoft.VisualStudio.TestTools.UnitTesting"];
}
