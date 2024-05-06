using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries.Tests;

public class MockingLibraryProjectLocatorTests
{
    [Fact]
    public void TestGetFakeItEasyMockingLibrary()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/FakeItEasyProject/FakeItEasyProject.csproj");

        // Act
        var result = MockingLibraryProjectLocator.GetMockingLibrary(testProject);

        // Assert
        Assert.Equal(MockingLibrary.FakeItEasy, result);
    }

    [Fact]
    public void TestGetMoqMockingLibrary()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/MoqProject/MoqProject.csproj");

        // Act
        var result = MockingLibraryProjectLocator.GetMockingLibrary(testProject);

        // Assert
        Assert.Equal(MockingLibrary.Moq, result);
    }

    [Fact]
    public void TestGetNSubstituteMockingLibrary()
    {
        // Arrange
        var testProject = new CodeProject("WorkspaceFake/MockingLibraries/NSubstituteProject/NSubstituteProject.csproj");

        // Act
        var result = MockingLibraryProjectLocator.GetMockingLibrary(testProject);

        // Assert
        Assert.Equal(MockingLibrary.NSubstitute, result);
    }
}
