using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Commands.Helpers;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestsCommandHandler(DTE2 dte, CodeProject testProject) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();

        var projectItem = (ProjectItem)selectedItems[0].Object;
        var sourceProject = projectItem.ContainingProject;
        var roslynProject = await projectItem.FindRoslynProjectAsync();
        var compilation = await roslynProject.GetCompilationAsync();

        var targetProject = await GetTargetProjectAsync(selectedItems);

        foreach (var selectedItem in selectedItems)
        {
            projectItem = (ProjectItem)selectedItem.Object;
            string sourceFileName = projectItem.FileNames[0];
            var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);
            var syntaxTreeRoot = await syntaxTree.GetRootAsync();

            var availableTypeSymbols = SymbolHelper.GetAvailableTypeSymbolsForGeneration(syntaxTreeRoot, compilation).ToImmutableArray();

            if (availableTypeSymbols.Length > 1 && !SymbolHelper.ShowMultipleTypesWarning(sourceFileName, availableTypeSymbols))
                continue;

            foreach (var typeSymbol in availableTypeSymbols)
            {
                var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(typeSymbol, sourceProject);
                await codeCoordinator.LoadSourceCodeAsync(targetProject);
            }
        }
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
