using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Code.TypeSymbolProviders;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands.Handlers;

internal class GenerateTestsAdvancedCommandHandler(DTE2 dte) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var projectItem = (ProjectItem)dte.GetSelectedItemsFromSolutionExplorer()[0].Object;
        var sourceProject = await projectItem.FindRoslynProjectAsync();

        string sourceFileName = projectItem.FileNames[0];
        var compilation = await sourceProject.GetCompilationAsync();
        var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);

        TestSourceCodeLoadingContext codeLoadingContext = new()
        {
            DTE = dte,
            TypeSymbolProvider = new SyntaxTreeTypeSymbolProvider(compilation, syntaxTree)
        };

        await TestSourceCodeUtility.LoadSourceCodeFromAdvancedViewAsync(dte, codeLoadingContext);
    }
}
