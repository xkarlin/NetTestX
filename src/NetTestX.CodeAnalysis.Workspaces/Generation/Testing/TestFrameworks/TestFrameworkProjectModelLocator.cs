using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

/// <summary>
/// Helper class used to locate instances of <see cref="ITestFrameworkProjectModel"/> for <see cref="TestFramework"/>
/// </summary>
public static class TestFrameworkProjectModelLocator
{
    /// <summary>
    /// Locate an instance of <see cref="ITestFrameworkProjectModel"/> for the given <paramref name="framework"/>
    /// </summary>
    public static ITestFrameworkProjectModel LocateModel(TestFramework framework) => framework switch
    {
        TestFramework.XUnit => new XUnitProjectModel(),
        TestFramework.NUnit => new NUnitProjectModel(),
        TestFramework.MSTest => new MSTestProjectModel(),
        _ => throw new NotSupportedException()
    };
}
