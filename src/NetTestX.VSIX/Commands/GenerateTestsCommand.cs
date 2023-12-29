using System;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX;

[Command(PackageIds.GenerateTestsCommand)]
internal sealed class GenerateTestsCommand : BaseCommand<GenerateTestsCommand>
{
    private DTE2 _dte;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        await VS.MessageBox.ShowWarningAsync("NetTestX.VSIX", "Button clicked");
    }

    protected override void BeforeQueryStatus(EventArgs e)
    {
        bool visible = ShouldCommandBeVisible();

        Command.Visible = Command.Enabled = visible;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var selectedItems = (UIHierarchyItem[])_dte.ToolWindows.SolutionExplorer.SelectedItems;

        if (selectedItems.Length != 1)
            return false;

        var projectItem = (ProjectItem)selectedItems[0].Object;

        return projectItem.FileNames[0].EndsWith(".cs");
    }
}
