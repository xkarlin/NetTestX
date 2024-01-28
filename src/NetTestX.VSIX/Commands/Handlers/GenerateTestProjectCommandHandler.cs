using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestProjectCommandHandler(DTE2 dte) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = dte.GetSelectedProjectFromSolutionExplorer()
        };

        await TestProjectUtility.CreateTestProjectFromViewAsync(context);
    }
}
