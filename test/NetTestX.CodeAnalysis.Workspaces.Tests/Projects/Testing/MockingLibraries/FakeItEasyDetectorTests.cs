using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries.Tests;

public class FakeItEasyDetectorTests
{
    [Fact]
    public void TestDetect()
    {
        // Arrange
        FakeItEasyDetector sut = new();

        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/FakeItEasyProject/FakeItEasyProject.csproj");

        // Act
        var result = sut.Detect(testProject);

        // Assert
        Assert.True(result);
    }
}
