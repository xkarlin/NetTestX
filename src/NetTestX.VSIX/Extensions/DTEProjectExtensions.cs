using System.IO;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class DTEProjectExtensions
{
    public static async Task<RoslynProject> FindRoslynProjectAsync(this DTEProject project)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();
        var sourceProject = workspace.FindProjectByName(project.Name);
        return sourceProject;
    }

    public static async Task CreateProjectFileAsync(this DTEProject project, Stream stream, string fileName)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string filePath = $"{Path.GetDirectoryName(project.FullName)}/{fileName}";

        using (var fs = File.Create(filePath))
            await stream.CopyToAsync(fs);

        project.ProjectItems.AddFromFile(filePath);
    }
}
