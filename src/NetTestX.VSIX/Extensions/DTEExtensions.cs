using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class DTEExtensions
{
    public static UIHierarchyItem[] GetSelectedItemsFromSolutionExplorer(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        return (UIHierarchyItem[])dte.ToolWindows.SolutionExplorer.SelectedItems;
    }

    public static IEnumerable<Project> GetSolutionProjects(this DTE2 dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        return dte.Solution.Projects.Cast<Project>();
    }
}
