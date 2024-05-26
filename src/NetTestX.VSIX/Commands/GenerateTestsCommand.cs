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

/// <summary>
/// Command that generates tests the fast way using default options, uses <see cref="GenerateTestsCommandHandler"/>
/// </summary>
[Command(PackageIds.GenerateTestsCommand)]
internal sealed class GenerateTestsCommand : BaseDynamicCommand<GenerateTestsCommand, GenerateTestsCommandHandler, CodeProject>
{
    protected override GenerateTestsCommandHandler CreateHandler(CodeProject item) => new(DTE, item);

    protected override void BeforeQueryStatus(OleMenuCommand command, EventArgs e, CodeProject project)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        bool visible = ShouldCommandBeVisible();

        Command.Visible = Command.Enabled = visible;
        
        command.Text = project is null ? "Generate Test Project..." : $"In {project.Name}";
    }

    protected override IReadOnlyList<CodeProject> GetItems()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        List<CodeProject> projects = [null];

        if (ShouldCommandBeVisible())
        {
            var solution = CodeWorkspace.Open(DTE.Solution.FileName);

            var testProjects = solution.GetTestProjects().ToArray();
            projects.AddRange(testProjects);
        }

        return projects;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var selectedItems = (UIHierarchyItem[])DTE.ToolWindows.SolutionExplorer.SelectedItems;

        if (selectedItems.Length == 0)
            return false;

        return selectedItems.All(x =>
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return x.Object is ProjectItem item && item.FileNames[0].EndsWith(SourceFileExtensions.CSHARP_DOT);
        });
    }
}
