using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="ProjectItem"/>
/// </summary>
public static class ProjectItemExtensions
{
    /// <summary>
    /// Find the corresponsind <see cref="RoslynProject"/> for the project that conatins this <paramref name="item"/>
    /// </summary>
    public static async Task<RoslynProject> FindRoslynProjectAsync(this ProjectItem item)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();
        var sourceProject = workspace.FindProjectByName(item.ContainingProject.Name);
        return sourceProject;
    }
}
