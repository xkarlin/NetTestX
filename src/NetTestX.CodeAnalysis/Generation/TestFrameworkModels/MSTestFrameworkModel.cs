using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

internal class MSTestFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => "TestClass";

    public string TestMethodAttribute => "TestMethod";

    public IEnumerable<string> CollectNamespaces() => ["Microsoft.VisualStudio.TestTools.UnitTesting"];
}
