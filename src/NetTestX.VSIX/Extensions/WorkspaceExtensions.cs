using System.Linq;
using Microsoft.CodeAnalysis;

namespace NetTestX.VSIX.Extensions;

public static class WorkspaceExtensions
{
    public static Project FindProjectByName(this Workspace workspace, string name)
    {
        return workspace.CurrentSolution.Projects.FirstOrDefault(x => x.Name == name);
    }
}
