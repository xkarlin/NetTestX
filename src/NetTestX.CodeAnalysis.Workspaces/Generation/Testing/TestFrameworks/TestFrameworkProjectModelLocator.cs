using System;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

public static class TestFrameworkProjectModelLocator
{
    public static ITestFrameworkProjectModel LocateModel(TestFramework framework) => framework switch
    {
        TestFramework.XUnit => new XUnitProjectModel(),
        TestFramework.NUnit => new NUnitProjectModel(),
        _ => throw new NotSupportedException()
    };
}
