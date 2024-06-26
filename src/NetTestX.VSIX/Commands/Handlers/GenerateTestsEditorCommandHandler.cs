﻿using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

/// <summary>
/// Handler for generating tests the fast way from the editor window, using default options
/// </summary>
public class GenerateTestsEditorCommandHandler(DTE2 dte, INamedTypeSymbol typeSymbol, CodeProject testProject) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var sourceProject = dte.ActiveDocument.ProjectItem.ContainingProject;
        var targetProject = await GetTargetProjectAsync();

        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(typeSymbol, sourceProject);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }

    private async Task<DTEProject> GetTargetProjectAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        if (testProject is not null)
            return dte.Solution.FindSolutionProject(testProject.Name);

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = dte.ActiveDocument.ProjectItem.ContainingProject
        };
        
        return await TestProjectUtility.CreateTestProjectFromViewAsync(context);
    }
}
