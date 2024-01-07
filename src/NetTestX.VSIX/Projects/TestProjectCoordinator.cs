using System;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Models;
using NetTestX.VSIX.UI.Views;
using DTEProject = EnvDTE.Project;

namespace NetTestX.VSIX.Projects;

public class TestProjectCoordinator
{
    public async Task<DTEProject> LoadTestProjectAsync(TestProjectCoordinatorContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        if (context.TestProject is { } testProject)
            return context.DTE.Solution.FindSolutionProject(testProject.Name);

        GenerateTestProjectModel generateTestModel = new();
        GenerateTestProjectView generateTestDialog = new(generateTestModel);

        var dialogResult = generateTestDialog.ShowDialog();

        if (dialogResult != true)
            return null;

        TestProjectFactory factory = new();
        TestProjectFactoryContext factoryContext = new()
        {
            DTE = context.DTE,
            Project = context.CurrentProject
        };

        return await factory.CreateTestProjectAsync(factoryContext, generateTestModel);
    }
}
