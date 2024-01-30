using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsEditorCommand)]
public class GenerateTestsEditorCommand : BaseDynamicCommand<GenerateTestsEditorCommand, GenerateTestsEditorCommandHandler, CodeProject>
{
    private DTE2 _dte;

    private INamedTypeSymbol _activeTypeSymbol;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override GenerateTestsEditorCommandHandler CreateHandler(CodeProject item) => new(_dte, _activeTypeSymbol, item);

    protected override void BeforeQueryStatus(OleMenuCommand command, EventArgs e, CodeProject project)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        bool visible = ShouldCommandBeVisible();

        Command.Visible = Command.Enabled = visible;

        if (!visible)
            return;

        command.Text = project is null ? "Generate Test Project..." : $"In {project.Name}";
    }

    protected override IReadOnlyList<CodeProject> GetItems()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        List<CodeProject> projects = [null];

        var solution = CodeWorkspace.Open(_dte.Solution.FileName);

        var testProjects = solution.GetTestProjects().ToArray();
        projects.AddRange(testProjects);

        return projects;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var textView = Package.GetActiveTextView();

        return textView.TryGetActiveTypeSymbol(out _activeTypeSymbol);
    }
}
