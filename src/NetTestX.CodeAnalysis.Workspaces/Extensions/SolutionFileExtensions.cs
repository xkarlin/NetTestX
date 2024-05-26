using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Construction;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions;

/// <summary>
/// Extensions for <see cref="SolutionFile"/>
/// </summary>
public static class SolutionFileExtensions
{
    /// <summary>
    /// Get all projects that this <see cref="SolutionFile"/> contains
    /// </summary>
    public static IEnumerable<ProjectInSolution> GetSolutionProjects(this SolutionFile solution)
    {
        return solution.ProjectsInOrder.Where(x => x.ProjectType
            is SolutionProjectType.KnownToBeMSBuildFormat
            or SolutionProjectType.WebProject);
    }
}
