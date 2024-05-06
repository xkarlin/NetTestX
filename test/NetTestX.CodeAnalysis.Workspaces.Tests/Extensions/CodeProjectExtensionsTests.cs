using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.CodeAnalysis.Workspaces.Generation;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions.Tests;

public class CodeProjectExtensionsTests
{
    [Fact]
    public void TestGetPackageReferences()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/FakeItEasyProject/FakeItEasyProject.csproj");

        // Act
        var result = CodeProjectExtensions.GetPackageReferences(testProject);

        // Assert
        Assert.Equal([new("PackageReference", "FakeItEasy")], result, EqualityComparer<CodeProjectItem>.Default);
    }

    [Fact]
    public void TestIsTestProject()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/Misc/Project.csproj");

        // Act
        var result = CodeProjectExtensions.IsTestProject(testProject);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestGetProjectTestFramework()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/MSTestProject/MSTestProject.csproj");

        // Act
        var result = CodeProjectExtensions.GetProjectTestFramework(testProject);

        // Assert
        Assert.Equal(TestFramework.MSTest, result);
    }

    [Fact]
    public void TestGetProjectMockingLibrary()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/FakeItEasyProject/FakeItEasyProject.csproj");

        // Act
        var result = CodeProjectExtensions.GetProjectMockingLibrary(testProject);

        // Assert
        Assert.Equal(MockingLibrary.FakeItEasy, result);
    }
}
