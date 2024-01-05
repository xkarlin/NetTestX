﻿using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

public static class TestFrameworkProjectModelLocator
{
    public static ITestFrameworkProjectModel LocateModel(TestFramework framework) => framework switch
    {
        TestFramework.XUnit => new XUnitProjectModel(),
        TestFramework.NUnit => new NUnitProjectModel(),
        _ => throw new NotSupportedException()
    };
}
