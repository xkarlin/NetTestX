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
using NetTestX.VSIX.Options;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestsCommandHandler(DTE2 dte, CodeProject testProject) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();

        ProjectItem[] items = selectedItems.Select(x =>
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return (ProjectItem)x.Object;
        }).ToArray();

        var projectItem = (ProjectItem)selectedItems[0].Object;
        var sourceProject = projectItem.ContainingProject;
        var roslynProject = await projectItem.FindRoslynProjectAsync();
        var compilation = await roslynProject.GetCompilationAsync();

        if (testProject is not null)
        {
            var dteProject = dte.Solution.FindSolutionProject(testProject.Name);
            await GenerateSourceFilesAsync(dteProject);
            return;
        }

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = ((ProjectItem)selectedItems[0].Object).ContainingProject,
            SaveCallback = GenerateSourceFilesAsync
        };

        await TestProjectUtility.CreateTestProjectFromViewAsync(context);

        async Task GenerateSourceFilesAsync(DTEProject targetProject)
        {
            var advancedOptions = await AdvancedOptions.GetLiveInstanceAsync();

            foreach (var item in items)
            {
                string sourceFileName = item.FileNames[0];
                var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);
                var syntaxTreeRoot = await syntaxTree.GetRootAsync();

                var availableTypeSymbols = SymbolHelper.GetAvailableTypeSymbolsForGeneration(syntaxTreeRoot, compilation).ToImmutableArray();

                bool shouldShowWarning = advancedOptions.ShowMultipleTypeWarning && availableTypeSymbols.Length > 1;

                if (shouldShowWarning && !SymbolHelper.ShowMultipleTypesWarning(sourceFileName, availableTypeSymbols))
                    continue;

                foreach (var typeSymbol in availableTypeSymbols)
                {
                    var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(typeSymbol, sourceProject);
                    await codeCoordinator.LoadSourceCodeAsync(targetProject);
                }
            }
        }
    }
}
