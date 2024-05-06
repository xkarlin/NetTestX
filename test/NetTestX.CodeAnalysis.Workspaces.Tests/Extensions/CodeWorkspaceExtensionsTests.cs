using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NSubstitute;
using Xunit;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing;
using Microsoft.Build.Locator;
using System.Runtime.CompilerServices;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions.Tests;

public class CodeWorkspaceExtensionsTests
{
    [Fact]
    public async Task TestCreateTestProjectAsync()
    {
        // Arrange
        var testWorkspace = CodeWorkspace.Open(Environment.CurrentDirectory + @"\WorkspaceFake\Workspace.sln");

        var testContext = new TestProjectCreationContext()
        {
            MockingLibrary = Common.MockingLibrary.NSubstitute,
            TestFramework = Common.TestFramework.NUnit,
            ProjectName = "TestingProject",
            OriginalProjectPath = Environment.CurrentDirectory + @"\WorkspaceFake\Misc\Project.csproj",
            ProjectFilePath = Environment.CurrentDirectory + @"\WorkspaceFake\Misc\TestingProject.csproj"
        };

        var testSaveCallback = () => Task.CompletedTask;

        // Act
        var result = await CodeWorkspaceExtensions.CreateTestProjectAsync(testWorkspace, testContext, testSaveCallback);

        // Assert
        Assert.Equal(Environment.CurrentDirectory + @"\WorkspaceFake\Misc\TestingProject.csproj", result.FilePath);
    }

    [Fact]
    public void TestGetTestProjects()
    {
        // Arrange
        var testWorkspace = CodeWorkspace.Open(Environment.CurrentDirectory + @"\WorkspaceFake\Workspace.sln");

        // Act
        var result = CodeWorkspaceExtensions.GetTestProjects(testWorkspace);

        // Assert
        Assert.Single(result);
        Assert.Equal(["Project"], result.Select(x => x.Name));
    }
}
