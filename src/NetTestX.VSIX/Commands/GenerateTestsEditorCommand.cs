using System.Collections.Generic;
using System;
using System.Linq;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Commands.Helpers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsEditorCommand)]
public class GenerateTestsEditorCommand : BaseDynamicCommand<GenerateTestsEditorCommand, GenerateTestsEditorCommandHandler, CodeProject>
{
    private INamedTypeSymbol _activeTypeSymbol;

    protected override GenerateTestsEditorCommandHandler CreateHandler(CodeProject item) => new(DTE, _activeTypeSymbol, item);

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

        var solution = CodeWorkspace.Open(DTE.Solution.FileName);

        var testProjects = solution.GetTestProjects().ToArray();
        projects.AddRange(testProjects);

        return projects;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var textView = Package.GetActiveTextView();

        return textView.TryGetActiveTypeSymbol(out _activeTypeSymbol) && SymbolHelper.CanGenerateTestsForTypeSymbol(_activeTypeSymbol);
    }
}
