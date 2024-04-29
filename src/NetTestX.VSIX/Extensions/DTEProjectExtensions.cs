using System;
using System.IO;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

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

    public static async Task CreateProjectFileAsync(this DTEProject project, Stream stream, string filePath)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        using (var fs = File.Create(filePath))
            await stream.CopyToAsync(fs);

        project.ProjectItems.AddFromFile(filePath);
    }

    public static Guid GetProjectId(this DTEProject project)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));
        IVsHierarchy hierarchy;

        solution.GetProjectOfUniqueName(project.FullName, out hierarchy);

        if (hierarchy != null)
        {
            hierarchy.GetGuidProperty(
                        VSConstants.VSITEMID_ROOT,
                        (int)__VSHPROPID.VSHPROPID_ProjectIDGuid,
                        out var projectGuid);

            return projectGuid;
        }

        return Guid.Empty;
    }
}
