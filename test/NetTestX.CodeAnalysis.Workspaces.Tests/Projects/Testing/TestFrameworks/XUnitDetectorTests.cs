using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks.Tests;

public class XUnitDetectorTests
{
    [Fact]
    public void TestDetect()
    {
        // Arrange
        XUnitDetector sut = new();

        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/XUnitProject/XUnitProject.csproj");

        // Act
        var result = sut.Detect(testProject);

        // Assert
        Assert.True(result);
    }
}
