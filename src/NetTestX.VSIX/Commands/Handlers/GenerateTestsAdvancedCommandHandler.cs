using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Commands.Helpers;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Options;
using ProjectItem = EnvDTE.ProjectItem;

namespace NetTestX.VSIX.Commands.Handlers;

/// <summary>
/// Handler for generating tests the advanced way using a dedicated window
/// </summary>
public class GenerateTestsAdvancedCommandHandler(DTE2 dte) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var projectItems = dte.GetSelectedItemsFromSolutionExplorer();
        var sourceProject = ((ProjectItem)projectItems[0].Object).ContainingProject;
        var roslynProject = await sourceProject.FindRoslynProjectAsync();

        var advancedOptions = await AdvancedOptions.GetLiveInstanceAsync();

        foreach (var projectItem in projectItems)
        {
            string sourceFileName = ((ProjectItem)projectItem.Object).FileNames[0];
            var compilation = await roslynProject.GetCompilationAsync();
            var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);
            var syntaxTreeRoot = await syntaxTree.GetRootAsync();

            var availableTypeSymbols = SymbolHelper.GetAvailableTypeSymbolsForGeneration(syntaxTreeRoot, compilation).ToImmutableArray();

            bool shouldShowWarning = advancedOptions.ShowMultipleTypeWarning && availableTypeSymbols.Length > 1;

            if (shouldShowWarning && !SymbolHelper.ShowMultipleTypesWarning(sourceFileName, availableTypeSymbols))
                continue;

            foreach (var typeSymbol in availableTypeSymbols)
                await TestSourceCodeUtility.LoadSourceCodeFromAdvancedViewAsync(dte, sourceProject, typeSymbol);
        }
    }
}
