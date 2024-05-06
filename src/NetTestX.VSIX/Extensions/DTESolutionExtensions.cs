using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace NetTestX.VSIX.Extensions;

public static class DTESolutionExtensions
{
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

    public static Project FindSolutionProject(this Solution solution, string name)
    {
        return solution.GetSolutionProjects().FirstOrDefault(x =>
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return x.Name == name;
        });
    }
}
