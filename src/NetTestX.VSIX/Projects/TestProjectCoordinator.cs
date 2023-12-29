using System.Linq;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using DTEProject = EnvDTE.Project;

namespace NetTestX.VSIX.Projects;

public class TestProjectCoordinator(DTE2 dte)
{
    public async Task<DTEProject> LoadTestProjectAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var targetProject = dte.GetSolutionProjects().FirstOrDefault(x =>
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return x.Name.EndsWith(".Tests");
        });

        return targetProject;
    }
}
