﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;
using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing;
using NetTestX.CodeAnalysis.Workspaces.Templates;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions;

/// <summary>
/// Extensions for <see cref="CodeWorkspace"/>
/// </summary>
public static class CodeWorkspaceExtensions
{
    /// <summary>
    /// Generate a <see cref="CodeProject"/> in this <paramref name="workspace"/> using the provided <paramref name="context"/>
    /// </summary>
    public static async Task<CodeProject> CreateTestProjectAsync(this CodeWorkspace workspace, TestProjectCreationContext context, Func<Task> saveCallback)
    {
        var mockingLibraryModel = MockingLibraryProjectModelLocator.LocateModel(context.MockingLibrary);
        var testFrameworkModel = TestFrameworkProjectModelLocator.LocateModel(context.TestFramework);

        string originalProjectRelativePath = new Uri(context.ProjectFilePath)
            .MakeRelativeUri(new(context.OriginalProjectPath))
            .OriginalString;

        TestProjectModel model = new()
        {
            ProjectName = context.ProjectName,
            OriginalProjectRelativePath = originalProjectRelativePath,
            MockingLibraryModel = mockingLibraryModel,
            TestFrameworkModel = testFrameworkModel
        };

        RazorFileTemplate template = new(model);
        string projectFile = await template.RenderAsync();

        var project = await workspace.CreateProjectAsync(context.ProjectFilePath, projectFile, saveCallback);
        return project;
    }

    /// <summary>
    /// Get all test projects that exist in this <see cref="CodeWorkspace"/>
    /// </summary>
    public static IEnumerable<CodeProject> GetTestProjects(this CodeWorkspace workspace)
        => workspace.Projects.Where(CodeProjectExtensions.IsTestProject);
}
