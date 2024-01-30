using System;
using System.Collections.Generic;
using System.Linq;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsCommand)]
internal sealed class GenerateTestsCommand : BaseDynamicCommand<GenerateTestsCommand, GenerateTestsCommandHandler, CodeProject>
{
    protected override GenerateTestsCommandHandler CreateHandler(CodeProject item) => new(DTE, item);

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

        var selectedItems = (UIHierarchyItem[])DTE.ToolWindows.SolutionExplorer.SelectedItems;

        if (selectedItems.Length != 1)
            return false;

        var projectItem = (ProjectItem)selectedItems[0].Object;

        return projectItem.FileNames[0].EndsWith(SourceFileExtensions.CSHARP_DOT);
    }
}
