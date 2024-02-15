using System.Linq;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;
using NetTestX.Polyfill.Extensions;

namespace NetTestX.VSIX.Code;

public static class TestSourceCodeUtility
{
    public static async Task LoadSourceCodeFromAdvancedViewAsync(DTE2 dte, DTEProject sourceProject, INamedTypeSymbol typeSymbol)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        TestSourceCodeDiagnosticReporter reporter = new();
        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(typeSymbol, sourceProject, reporter);

        GenerateTestsAdvancedModel model = new()
        {
            TestFileName = codeCoordinator.Options.TestFileName,
            TestClassName = codeCoordinator.DriverBuilder.TestClassName,
            TestClassNamespace = codeCoordinator.DriverBuilder.TestClassNamespace,
            TestMethodMap = new(codeCoordinator.DriverBuilder.TestMethodMap)
        };

        var workspace = CodeWorkspace.Open(dte.Solution.FileName);
        var testProjects = workspace.GetTestProjects();

        GenerateTestsAdvancedView view = new(model, reporter, testProjects.Select(x => x.Name));

        bool? result = view.ShowDialog();

        if (result != true)
            return;

        codeCoordinator.Options.TestFileName = model.TestFileName;
        codeCoordinator.DriverBuilder.TestClassName = model.TestClassName;
        codeCoordinator.DriverBuilder.TestClassNamespace = model.TestClassNamespace;

        foreach (var (modelBase, enabled) in model.TestMethodMap)
            codeCoordinator.DriverBuilder.TestMethodMap[modelBase] = enabled;

        var targetProject = dte.Solution.FindSolutionProject(model.TestProject);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }
}
