using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class DTEExtensions
{
    public static Project GetSelectedProjectFromSolutionExplorer(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();
        return (Project)selectedItems[0].Object;
    }

    public static UIHierarchyItem[] GetSelectedItemsFromSolutionExplorer(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        return (UIHierarchyItem[])dte.ToolWindows.SolutionExplorer.SelectedItems;
    }
}
