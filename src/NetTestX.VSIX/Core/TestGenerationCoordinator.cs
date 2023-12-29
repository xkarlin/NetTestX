using System.Threading.Tasks;
using EnvDTE80;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Core;

public class TestGenerationCoordinator(DTE2 dte)
{
    private readonly TestProjectCoordinator _projectCoordinator = new(dte);

    private readonly TestSourceCodeCoordinator _codeCoordinator = new(dte);

    public async Task ProcessTestGenerationRequestAsync()
    {
        var project = await _projectCoordinator.LoadTestProjectAsync();
        await _codeCoordinator.LoadTestSourceCodeAsync(project);
    }
}
