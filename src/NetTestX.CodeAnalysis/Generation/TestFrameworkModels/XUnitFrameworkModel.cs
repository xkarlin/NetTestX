using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public class XUnitFrameworkModel : ITestFrameworkModel
{
    public string TestMethodAttribute => "Fact";

    public IEnumerable<string> CollectNamespaces() => ["Xunit"];
}
