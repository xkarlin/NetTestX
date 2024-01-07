using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Models;
using NetTestX.CodeAnalysis.Workspaces.Extensions;

namespace NetTestX.VSIX.Projects;

public class TestProjectFactory
{
    public async Task<Project> CreateTestProjectAsync(TestProjectFactoryContext context, GenerateTestProjectModel model)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string originalProjectPath = context.Project.FileName;

        string testProjectPath = $"{model.ProjectDirectory}/{model.ProjectName}/{model.ProjectName}.csproj";

        var workspace = CodeWorkspace.Open(context.DTE.Solution.FileName);

        TestProjectCreationContext ctx = new()
        {
            ProjectFilePath = testProjectPath,
            OriginalProjectPath = originalProjectPath,
            ProjectName = model.ProjectName,
            MockingLibrary = model.MockingLibrary,
            TestFramework = model.TestFramework
        };

        await workspace.CreateTestProjectAsync(ctx, () => SaveSolutionAsync(context, testProjectPath));

        var targetProject = context.DTE.Solution.FindSolutionProject(model.ProjectName);

        return targetProject;
    }

    private static async Task SaveSolutionAsync(TestProjectFactoryContext context, string testProjectPath)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        context.DTE.Solution.AddFromFile(testProjectPath);
        context.DTE.Solution.SaveAs(context.DTE.Solution.FileName);
    }
}
