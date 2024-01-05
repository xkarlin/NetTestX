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
    public async Task<DTEProject> LoadTestProjectAsync(TestProjectLoadingContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string originalProjectName = context.Project.Name;
        string testProjectName = $"{originalProjectName}.Tests";

        if (context.DTE.Solution.FindSolutionProject(testProjectName) is not { } targetProject)
        {
            GenerateTestProjectModel generateTestModel = new();
            GenerateTestProjectView generateTestDialog = new(generateTestModel);

            var dialogResult = generateTestDialog.ShowDialog();

            if (dialogResult != true)
                return null;

            TestProjectFactory factory = new();
            targetProject = await factory.CreateTestProjectAsync(context, generateTestModel);
        }

        return targetProject;
    }
}
