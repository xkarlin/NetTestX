using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces;
using NSubstitute;
using Xunit;

namespace NetTestX.CodeAnalysis.Workspaces.Tests;

public class CodeWorkspaceTests
{
    [Fact]
    public void TestOpen()
    {
        // Arrange
        var testSolutionFilePath = Environment.CurrentDirectory + @"\WorkspaceFake\Workspace.sln";

        // Act
        var result = CodeWorkspace.Open(testSolutionFilePath);

        // Assert
        Assert.Equal(7, result.Projects.Count());
        Assert.Equal(
        [
            "Project",
            "FakeItEasyProject",
            "MoqProject",
            "NSubstituteProject",
            "MSTestProject",
            "NUnitProject",
            "XUnitProject"
        ], result.Projects.Select(x => x.Name));
    }
}
