using System.Collections.Generic;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

[TestFramework(TestFramework.NUnit)]
public class NUnitFrameworkModel : ITestFrameworkModel
{
    public string TestMethodAttribute => "Test";

    public IEnumerable<string> CollectNamespaces() => ["NUnit.Framework"];
}
