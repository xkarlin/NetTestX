using System;
using System.Linq;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedCommand)]
internal sealed class GenerateTestsAdvancedCommand : BaseCommand<GenerateTestsAdvancedCommand, GenerateTestsAdvancedCommandHandler>
{
    protected override GenerateTestsAdvancedCommandHandler CreateHandler() => new(DTE);

    protected override void BeforeQueryStatus(EventArgs e)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var (visible, enabled) = ShouldCommandBeVisible();

        Command.Visible = visible;
        Command.Enabled = enabled;
    }

    private (bool Visible, bool Enabled) ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var selectedItems = (UIHierarchyItem[])DTE.ToolWindows.SolutionExplorer.SelectedItems;

        if (selectedItems.Length != 1)
            return (false, false);

        var projectItem = (ProjectItem)selectedItems[0].Object;

        if (!projectItem.FileNames[0].EndsWith(SourceFileExtensions.CSHARP_DOT))
            return (false, false);

        var workspace = CodeWorkspace.Open(DTE.Solution.FileName);

        if (!workspace.GetTestProjects().Any())
            return (true, false);

        return (true, true);
    }
}
