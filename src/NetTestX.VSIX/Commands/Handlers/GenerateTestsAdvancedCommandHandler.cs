using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Commands.Helpers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands.Handlers;

internal class GenerateTestsAdvancedCommandHandler(DTE2 dte) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var projectItem = (ProjectItem)dte.GetSelectedItemsFromSolutionExplorer()[0].Object;
        var sourceProject = projectItem.ContainingProject;
        var roslynProject = await sourceProject.FindRoslynProjectAsync();

        string sourceFileName = projectItem.FileNames[0];
        var compilation = await roslynProject.GetCompilationAsync();
        var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);
        var syntaxTreeRoot = await syntaxTree.GetRootAsync();

        var availableTypeSymbols = SymbolHelper.GetAvailableTypeSymbolsForGeneration(syntaxTreeRoot, compilation).ToImmutableArray();

        if (availableTypeSymbols.Length > 1 && !SymbolHelper.ShowMultipleTypesWarning(sourceFileName, availableTypeSymbols))
            return;

        foreach (var typeSymbol in availableTypeSymbols)
            await TestSourceCodeUtility.LoadSourceCodeFromAdvancedViewAsync(dte, sourceProject, typeSymbol);
    }
}
