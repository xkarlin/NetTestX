using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="Solution"/>
/// </summary>
public static class DTESolutionExtensions
{
    /// <summary>
    /// Get all projects that this <see cref="Solution"/> contains
    /// </summary>
    public static IEnumerable<Project> GetSolutionProjects(this Solution solution)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        List<Project> result = [];

        var projects = solution.Projects.Cast<Project>();

        foreach (var project in projects)
            CollectProject(project);

        return result;

        void CollectProject(Project project)
        {
            if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
            {
                CollectSolutionFolder(project);
                return;
            }

            if (project.Object is VSProject)
                result.Add(project);
        }

        void CollectSolutionFolder(Project project)
        {
            foreach (var item in project.ProjectItems.Cast<ProjectItem>())
                CollectProject(item.SubProject);
        }
    }

    /// <summary>
    /// Find the project that has the specified <paramref name="name"/> in the <see cref="Solution"/>
    /// </summary>
    public static Project FindSolutionProject(this Solution solution, string name)
    {
        return solution.GetSolutionProjects().FirstOrDefault(x =>
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return x.Name == name;
        });
    }
}
