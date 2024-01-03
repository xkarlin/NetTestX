using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public static class TestFrameworkModelLocator
{
    public static ITestFrameworkModel LocateModel(TestFramework framework) => framework switch
    {
        TestFramework.XUnit => new XUnitFrameworkModel(),
        TestFramework.NUnit => new NUnitFrameworkModel(),
        _ => throw new NotSupportedException()
    };
}
