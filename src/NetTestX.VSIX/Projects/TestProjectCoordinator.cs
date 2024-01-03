using System;
using System.IO;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing;
using NetTestX.Common;
using NetTestX.VSIX.Extensions;
using DTEProject = EnvDTE.Project;

namespace NetTestX.VSIX.Projects;

public class TestProjectCoordinator
{
    public async Task<DTEProject> LoadTestProjectAsync(TestProjectLoadingContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItem = (ProjectItem)context.SelectedItems[0].Object;

        string solutionDirectory = Path.GetDirectoryName(context.DTE.Solution.FileName);
        string originalProjectPath = selectedItem.ContainingProject.FileName;
        string originalProjectName = selectedItem.ContainingProject.Name;
        string testProjectName = $"{originalProjectName}.Tests";
        string testProjectPath = $"{solutionDirectory}/{testProjectName}/{testProjectName}.csproj";

        var workspace = CodeWorkspace.Open(context.DTE.Solution.FileName);

        if (context.DTE.Solution.FindSolutionProject(testProjectName) is not { } targetProject)
        {
            TestProjectCreationContext ctx = new()
            {
                ProjectFilePath = testProjectPath,
                OriginalProjectPath = originalProjectPath,
                ProjectName = testProjectName,
                MockingLibrary = MockingLibrary.NSubstitute,
                TestFramework = TestFramework.XUnit
            };

            await workspace.CreateTestProjectAsync(ctx, () => SaveSolutionAsync(context, testProjectPath));

            targetProject = context.DTE.Solution.FindSolutionProject(testProjectName);
        }

        return targetProject;
    }

    private static async Task SaveSolutionAsync(TestProjectLoadingContext context, string testProjectPath)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        context.DTE.Solution.AddFromFile(testProjectPath);
        context.DTE.Solution.SaveAs(context.DTE.Solution.FileName);
    }
}
