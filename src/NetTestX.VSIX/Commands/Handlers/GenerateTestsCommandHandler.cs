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
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();

        var targetProject = await GetTargetProjectAsync(selectedItems);

        TestSourceCodeLoadingContext sourceCodeLoadingContext = new()
        {
            DTE = dte,
            SelectedItems = selectedItems
        };

        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(sourceCodeLoadingContext);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }

    private async Task<DTEProject> GetTargetProjectAsync(UIHierarchyItem[] selectedItems)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        if (testProject is not null)
            return dte.Solution.FindSolutionProject(testProject.Name);

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = ((ProjectItem)selectedItems[0].Object).ContainingProject
        };

        return await TestProjectUtility.CreateTestProjectFromViewAsync(context);
    }
}
