﻿using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

public class XUnitDetector : ITestFrameworkProjectDetector
{
    public TestFramework TestFramework => TestFramework.XUnit;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "xunit");
}
