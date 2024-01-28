using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class DTESolutionExtensions
{
    public static IEnumerable<Project> GetSolutionProjects(this Solution solution)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        return solution.Projects.Cast<Project>();
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
