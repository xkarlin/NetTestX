using System;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Common;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedCommand)]
internal sealed class GenerateTestsAdvancedCommand : BaseCommand<GenerateTestsAdvancedCommand, GenerateTestsAdvancedCommandHandler>
{
    protected override GenerateTestsAdvancedCommandHandler CreateHandler() => new(DTE);

    protected override void BeforeQueryStatus(EventArgs e)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        bool visible = ShouldCommandBeVisible();

        Command.Visible = Command.Enabled = visible;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var selectedItems = (UIHierarchyItem[])DTE.ToolWindows.SolutionExplorer.SelectedItems;

        if (selectedItems.Length != 1)
            return false;

        var projectItem = (ProjectItem)selectedItems[0].Object;

        return projectItem.FileNames[0].EndsWith(SourceFileExtensions.CSHARP_DOT);
    }
}
