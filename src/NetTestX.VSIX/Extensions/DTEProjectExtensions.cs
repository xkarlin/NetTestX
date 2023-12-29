using System.IO;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace NetTestX.VSIX.Extensions;

public static class DTEProjectExtensions
{
    public static async Task CreateProjectFileAsync(this Project project, Stream stream, string fileName)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        string filePath = $"{Path.GetDirectoryName(project.FullName)}/{fileName}";

        using (var fs = File.Create(filePath))
            await stream.CopyToAsync(fs);

        project.ProjectItems.AddFromFile(filePath);
    }
}
