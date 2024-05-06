using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NSubstitute;
using Xunit;
using Microsoft.Build.Construction;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions.Tests;

public class SolutionFileExtensionsTests
{
    [Fact]
    public void TestGetSolutionProjects()
    {
        // Arrange
        var testSolution = SolutionFile.Parse(Environment.CurrentDirectory + @"\WorkspaceFake\Workspace.sln");

        // Act
        var result = SolutionFileExtensions.GetSolutionProjects(testSolution);

        // Assert
        Assert.Equal(7, result.Count());
        Assert.Equal(
        [
            "Project",
            "FakeItEasyProject",
            "MoqProject",
            "NSubstituteProject",
            "MSTestProject",
            "NUnitProject",
            "XUnitProject"
        ], result.Select(x => x.ProjectName));
    }
}
