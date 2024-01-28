using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class ProjectItemExtensions
{
    public static async Task<RoslynProject> FindRoslynProjectAsync(this ProjectItem item)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();
        var sourceProject = workspace.FindProjectByName(item.ContainingProject.Name);
        return sourceProject;
    }
}
