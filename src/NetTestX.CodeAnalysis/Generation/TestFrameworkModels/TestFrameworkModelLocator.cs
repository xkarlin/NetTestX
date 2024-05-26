using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

/// <summary>
/// Helper class used to locate instances of <see cref="ITestFrameworkModel"/> based on <see cref="TestFramework"/>
/// </summary>
public static class TestFrameworkModelLocator
{
    /// <summary>
    /// Locate an instance of <see cref="ITestFrameworkModel"/> based on <paramref name="framework"/>
    /// </summary>
    public static ITestFrameworkModel LocateModel(TestFramework framework) => framework switch
    {
        TestFramework.XUnit => new XUnitFrameworkModel(),
        TestFramework.NUnit => new NUnitFrameworkModel(),
        TestFramework.MSTest => new MSTestFrameworkModel(),
        _ => throw new NotSupportedException()
    };
}
