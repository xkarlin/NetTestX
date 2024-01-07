﻿using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public class XUnitFrameworkModel : ITestFrameworkModel
{
    public string TestClassAttribute => null;

    public string TestMethodAttribute => "Fact";

    public IEnumerable<string> CollectNamespaces() => ["Xunit"];
}
