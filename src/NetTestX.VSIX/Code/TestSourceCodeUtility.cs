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
using System;
using System.IO;

namespace NetTestX.VSIX.Code;

/// <summary>
/// Helper class used for test source files generation
/// </summary>
public static class TestSourceCodeUtility
{
    /// <summary>
    /// Load the source code from advanced view (<see cref="GenerateTestsAdvancedView"/>)
    /// </summary>
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
            TestMethodMap = new(codeCoordinator.DriverBuilder.TestMethodMap),
            AdvancedOptions = codeCoordinator.DriverBuilder.AdvancedOptions
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
        codeCoordinator.DriverBuilder.AdvancedOptions = model.AdvancedOptions;

        foreach (var (modelBase, enabled) in model.TestMethodMap)
            codeCoordinator.DriverBuilder.TestMethodMap[modelBase] = enabled;

        var targetProject = dte.Solution.FindSolutionProject(model.TestProject);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }

    /// <summary>
    /// Rebase the relative path for <paramref name="targetBase"/> same way it's done with <paramref name="relativeBase"/> and <paramref name="sourcePath"/>
    /// </summary>
    public static string CopyRelativePath(string sourcePath, string relativeBase, string targetBase)
    {
        if (!sourcePath.EndsWith("/") && !sourcePath.EndsWith("\\"))
            sourcePath += "/";

        if (!relativeBase.EndsWith("/") && !relativeBase.EndsWith("\\"))
            relativeBase += "/";

        var relativePathUri = new Uri(relativeBase, UriKind.Absolute).MakeRelativeUri(new(sourcePath));
        string relativePath = relativePathUri.ToString();
        return Path.Combine(targetBase, relativePath);
    }
}
