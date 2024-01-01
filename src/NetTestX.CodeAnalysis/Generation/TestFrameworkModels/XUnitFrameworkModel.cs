using System.Collections.Generic;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

[TestFramework(TestFramework.XUnit)]
public class XUnitFrameworkModel : ITestFrameworkModel
{
    public string TestMethodAttribute => "Fact";

    public IEnumerable<string> CollectNamespaces() => ["Xunit"];
}
