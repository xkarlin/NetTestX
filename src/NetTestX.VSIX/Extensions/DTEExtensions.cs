using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="DTE2"/>
/// </summary>
public static class DTEExtensions
{
    /// <summary>
    /// Get the <see cref="DTEProject"/> currently selected in the Solution Explorer window
    /// </summary>
    public static Project GetSelectedProjectFromSolutionExplorer(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();
        return (Project)selectedItems[0].Object;
    }

    /// <summary>
    /// Get all <see cref="UIHierarchyItem"/>s currently selected in the Solution Explorer window
    /// </summary>
    public static UIHierarchyItem[] GetSelectedItemsFromSolutionExplorer(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        return (UIHierarchyItem[])dte.ToolWindows.SolutionExplorer.SelectedItems;
    }
}
