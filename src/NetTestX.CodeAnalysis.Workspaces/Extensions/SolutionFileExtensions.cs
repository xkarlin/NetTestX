using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Construction;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions;

public static class SolutionFileExtensions
{
    public static IEnumerable<ProjectInSolution> GetSolutionProjects(this SolutionFile solution)
    {
        return solution.ProjectsInOrder.Where(x => x.ProjectType
            is SolutionProjectType.KnownToBeMSBuildFormat
            or SolutionProjectType.WebProject);
    }
}
