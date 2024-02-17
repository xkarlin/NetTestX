using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.Common.Diagnostics;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Options;
using NetTestX.VSIX.Options.Parsing;

namespace NetTestX.VSIX.Code;

public class TestSourceCodeCoordinator
{
    private readonly DTEProject _sourceProject;

    public required TestSourceCodeCoordinatorOptions Options { get; init; }

    public required UnitTestGeneratorDriver.Builder DriverBuilder { get; init; }

    private TestSourceCodeCoordinator(DTEProject sourceProject)
    {
        _sourceProject = sourceProject;
    }

    public async Task LoadSourceCodeAsync(DTEProject targetProject)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        CodeProject codeProject = new(targetProject.FileName);

        var driver = GetGeneratorDriver(codeProject);

        string testSource = await driver.GenerateTestClassSourceAsync();

        string targetFilePath = GetTargetFilePath(targetProject);

        await AddSourceFileToProjectAsync(targetProject, testSource, targetFilePath);
    }

    public static async Task<TestSourceCodeCoordinator> CreateAsync(INamedTypeSymbol type, DTEProject sourceProject, IDiagnosticReporter reporter = null)
    {
        RoslynProject roslynProject = await sourceProject.FindRoslynProjectAsync();
        Compilation compilation = await roslynProject.GetCompilationAsync();

        var generalOptions = await General.GetLiveInstanceAsync();
        var advancedOptions = await Advanced.GetLiveInstanceAsync();

        TestSourceCodeCoordinator coordinator = new(sourceProject)
        {
            Options = new()
            {
                TestFileName = OptionResolverHelper.ResolveGeneralOption(generalOptions.TestFileName, type)
            },
            DriverBuilder = UnitTestGeneratorDriver.CreateBuilder(type, compilation, reporter)
        };

        coordinator.DriverBuilder.TestClassName = OptionResolverHelper.ResolveGeneralOption(generalOptions.TestClassName, type);
        coordinator.DriverBuilder.TestClassNamespace = OptionResolverHelper.ResolveGeneralOption(generalOptions.TestClassNamespace, type);
        coordinator.DriverBuilder.AdvancedOptions = advancedOptions.GetAdvancedGeneratorOptions();

        return coordinator;
    }

    private UnitTestGeneratorDriver GetGeneratorDriver(CodeProject project)
    {
        DriverBuilder.TestFramework = project.GetProjectTestFramework();
        DriverBuilder.MockingLibrary = project.GetProjectMockingLibrary();

        UnitTestGeneratorDriver driver = DriverBuilder.Build();
        return driver;
    }

    private async Task AddSourceFileToProjectAsync(DTEProject project, string sourceText, string fileName)
    {
        MemoryStream ms = new(Encoding.UTF8.GetBytes(sourceText));

        var targetFolder = Path.GetDirectoryName(fileName);
        Directory.CreateDirectory(targetFolder);

        await project.CreateProjectFileAsync(ms, fileName);
    }

    private string GetTargetFilePath(DTEProject targetProject)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        string testSourceFileName = $"{Options.TestFileName}.{SourceFileExtensions.CSHARP}";
        
        string sourceFileDirectory = Path.GetDirectoryName(DriverBuilder.Type.DeclaringSyntaxReferences[0].SyntaxTree.FilePath);
        string sourceProjectDirectory = Path.GetDirectoryName(_sourceProject.FileName);
        string targetProjectDirectory = Path.GetDirectoryName(targetProject.FileName);

        string targetFileDirectory = TestSourceCodeUtility.CopyRelativePath(sourceFileDirectory, sourceProjectDirectory, targetProjectDirectory);

        return Path.Combine(targetFileDirectory, testSourceFileName);
    }
}
