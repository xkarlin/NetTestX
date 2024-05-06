using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks.Tests;

public class TestFrameworkProjectLocatorTests
{
    [Fact]
    public void TestGetXUnitTestFramework()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/XUnitProject/XUnitProject.csproj");

        // Act
        var result = TestFrameworkProjectLocator.GetTestFramework(testProject);

        // Assert
        Assert.Equal(TestFramework.XUnit, result);
    }

    [Fact]
    public void TestGetNUnitTestFramework()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/NUnitProject/NUnitProject.csproj");

        // Act
        var result = TestFrameworkProjectLocator.GetTestFramework(testProject);

        // Assert
        Assert.Equal(TestFramework.NUnit, result);
    }

    [Fact]
    public void TestGetMSTestFramework()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/TestingFrameworks/MSTestProject/MSTestProject.csproj");

        // Act
        var result = TestFrameworkProjectLocator.GetTestFramework(testProject);

        // Assert
        Assert.Equal(TestFramework.MSTest, result);
    }
}
