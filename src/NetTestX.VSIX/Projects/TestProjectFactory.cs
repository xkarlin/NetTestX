using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing;
using NetTestX.Common;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Diagnostics;

namespace NetTestX.VSIX.Projects;

public class TestProjectFactory
{
    private readonly TestProjectFactoryContext _context;

    public required TestProjectFactoryOptions Options { get; init; }

    private TestProjectFactory(TestProjectFactoryContext context)
    {
        _context = context;
    }

    public async Task<DTEProject> CreateTestProjectAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string originalProjectPath = _context.Project.FileName;

        string testProjectPath = $"{Options.ProjectDirectory}/{Options.ProjectName}/{Options.ProjectName}.csproj";

        var workspace = CodeWorkspace.Open(_context.DTE.Solution.FileName);

        TestProjectCreationContext ctx = new()
        {
            ProjectFilePath = testProjectPath,
            OriginalProjectPath = originalProjectPath,
            ProjectName = Options.ProjectName,
            MockingLibrary = Options.MockingLibrary,
            TestFramework = Options.TestFramework
        };

        await workspace.CreateTestProjectAsync(ctx, () => SaveSolutionAsync(_context, testProjectPath));

        var targetProject = _context.DTE.Solution.FindSolutionProject(Options.ProjectName);

        return targetProject;
    }

    public static async Task<TestProjectFactory> CreateAsync(TestProjectFactoryContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string originalProjectPath = context.Project.FileName;
        string originalProjectName = context.Project.Name;

        return new(context)
        {
            Options = new()
            {
                ProjectName = $"{originalProjectName}.Tests",
                ProjectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(originalProjectPath)),
                TestFramework = TestFramework.NUnit,
                MockingLibrary = MockingLibrary.NSubstitute
            }
        };
    }

    private static async Task SaveSolutionAsync(TestProjectFactoryContext context, string testProjectPath)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string solutionPath = context.DTE.Solution.FullName;

        var project = context.DTE.Solution.AddFromFile(testProjectPath);
        context.DTE.Solution.SaveAs(context.DTE.Solution.FileName);

        await context.SaveCallback?.Invoke(project);

        _ = ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            using WaitDialogMessageLogger messageLogger = new("Creating Test Project");

            messageLogger.LogMessage("Waiting for project initialization...");

            context.DTE.Solution.Close();
            context.DTE.Solution.Open(solutionPath);
        });
    }
}
