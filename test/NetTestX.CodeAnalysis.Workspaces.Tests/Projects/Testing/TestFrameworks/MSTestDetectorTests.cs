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

public class MSTestDetectorTests
{
    [Fact]
    public void TestDetect()
    {
        // Arrange
        MSTestDetector sut = new();

        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/MSTestProject/MSTestProject.csproj");

        // Act
        var result = sut.Detect(testProject);

        // Assert
        Assert.True(result);
    }
}
