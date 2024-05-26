using System.Linq;
using Microsoft.CodeAnalysis;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="Workspace"/>
/// </summary>
public static class WorkspaceExtensions
{
    /// <summary>
    /// Find a <see cref="Project"/> with the provided <paramref name="name"/> inside the <see cref="Workspace"/>
    /// </summary>
    public static Project FindProjectByName(this Workspace workspace, string name)
    {
        return workspace.CurrentSolution.Projects.FirstOrDefault(x => x.Name == name);
    }
}
