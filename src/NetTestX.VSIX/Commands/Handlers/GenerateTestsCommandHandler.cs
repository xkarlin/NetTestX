using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestsCommandHandler(DTE2 dte, CodeProject testProject)
{
    private readonly TestProjectCoordinator _projectCoordinator = new();

    private readonly TestSourceCodeCoordinator _codeCoordinator = new();

    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();

        TestProjectCoordinatorContext projectLoadingContext = new()
        {
            DTE = dte,
            CurrentProject = ((ProjectItem)selectedItems[0].Object).ContainingProject,
            TestProject = testProject
        };

        var dteProject = await _projectCoordinator.LoadTestProjectAsync(projectLoadingContext);

        TestSourceCodeLoadingContext sourceCodeLoadingContext = new()
        {
            DTE = dte,
            SelectedItems = selectedItems,
            Project = dteProject
        };

        await _codeCoordinator.LoadTestSourceCodeAsync(sourceCodeLoadingContext);
    }
}
