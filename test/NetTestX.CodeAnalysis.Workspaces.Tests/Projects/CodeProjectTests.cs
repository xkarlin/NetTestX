using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NSubstitute;
using Xunit;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Tests;

public class CodeProjectTests
{
    [Fact]
    public void TestGetPropertyValue()
    {
        // Arrange
        var filePath = "WorkspaceFake/Misc/Project.csproj";
        CodeProject sut = new(filePath);

        var testName = "ImplicitUsings";

        // Act
        var result = sut.GetPropertyValue(testName);

        // Assert
        Assert.Equal("enable", result);
    }

    [Fact]
    public void TestAddItem()
    {
        // Arrange
        var filePath = "WorkspaceFake/Misc/Project.csproj";
        CodeProject sut = new(filePath);

        var testName = "Test";
        var testValue = "foo";

        // Act
        sut.AddItem(testName, testValue);

        // Assert
        Assert.Equal("foo", sut.GetItems("Test").First().Include);
    }

    [Fact]
    public void TestGetItems()
    {
        // Arrange
        var filePath = "WorkspaceFake/Misc/Project.csproj";
        CodeProject sut = new(filePath);

        var testName = "Test";

        // Act
        var result = sut.GetItems(testName);

        // Assert
        Assert.Empty(result);
    }
}
