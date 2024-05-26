using System;
using System.IO;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="DTEProject"/>
/// </summary>
public static class DTEProjectExtensions
{
    /// <summary>
    /// Find the corresponding <see cref="RoslynProject"/> for the provided <see cref="DTEProject"/>
    /// </summary>
    public static async Task<RoslynProject> FindRoslynProjectAsync(this DTEProject project)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();
        var sourceProject = workspace.FindProjectByName(project.Name);
        return sourceProject;
    }

    /// <summary>
    /// Create and add the file to the provided <paramref name="project"/> from the <paramref name="stream"/>
    /// </summary>
    public static async Task CreateProjectFileAsync(this DTEProject project, Stream stream, string filePath)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        using (var fs = File.Create(filePath))
            await stream.CopyToAsync(fs);

        project.ProjectItems.AddFromFile(filePath);
    }
}
